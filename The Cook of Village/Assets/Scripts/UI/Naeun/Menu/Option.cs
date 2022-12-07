using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    public Dropdown ResolutionDrop;
    public GameObject KeyboardOperate;
    public Toggle WindowToggle;
    public Slider BgmSlider, EffectSlider;
    public Image BgmImage, EffectImage;
    public Sprite[] BgmSprite;
    public Sprite[] EffectSprite;

    private Dictionary<int, Slider> DicSlider = new Dictionary<int, Slider>();
    private SoundManager soundManager;
    private bool BgmMute;
    private bool EffectMute;
    public void OptionUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (this.gameObject.activeSelf)
        {
            UIManager.UIOpenScaleAnimation(this.gameObject);
            OptionSetting();
        }
    }
    public void KeyboardState()
    {
        KeyboardOperate.SetActive(!KeyboardOperate.activeSelf);
    }
    private void Awake()
    {
        DicSlider.Add(0, BgmSlider);
        DicSlider.Add(1, EffectSlider);
        DicSlider.Add(2, EffectSlider);
    }
    private void OptionSetting()
    {
        soundManager = SoundManager.Instance;
        SoundInit();
        WindowInit();
    }
    private void SoundInit()
    {
        isStart = true;
        BgmMute = soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].mute;
        EffectMute = soundManager.audioSources[(int)SoundData.Type.Effect].audioSources[0].mute;
        if (!BgmMute)
        {
            BgmSlider.value = soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
        }
        else
        {
            MuteSound(0);
        }
        if (!EffectMute)
        {
            EffectSlider.value = soundManager.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
        }
        else
        {
            MuteSound(1);
        }
        isStart = false;
    }
    public void WindowInit()
    {
        ResolutionDrop.options.Clear();
        resolutions.Clear();
        resolutions = GetResolutions();
        foreach (var res in resolutions.Select((value, index) => (value, index)))
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = res.value.width + "X" + res.value.height;
            ResolutionDrop.options.Add(option);
            if (res.value.width == Screen.width && res.value.height == Screen.height)
            {
                ResolutionDrop.value = res.index;
            }
        }
        WindowToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.Windowed) ? true : false;
        ResolutionDrop.RefreshShownValue();
    }
    public List<Resolution> GetResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        HashSet<Tuple<int, int>> uniqResolutions = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++)
        {
            Tuple<int, int> resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqResolutions.Add(resolution);
            if (!maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            }
            else
            {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }
        }
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (Tuple<int, int> resolution in uniqResolutions)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
    }


    public void ChangeScreen()
    {
        int _width = resolutions[ResolutionDrop.value].width;
        int _height = resolutions[ResolutionDrop.value].height;
        Screen.SetResolution(_width, _height, !WindowToggle.isOn);
    }
    private bool StopBgmSlider;
    private bool StopEffectSlider;
    private bool isStart;
    /// <summary>
    /// 소리를 뮤트한다.
    /// </summary>
    /// <param name="type">Bgm인경우 0, Effect 또는 Effect3D인 경우 1</param>
    public void MuteSound(int type)
    {
        if (type.Equals(0)) //Bgm
        {
            if(!isStart)
            {
                BgmMute = !BgmMute;
                soundManager.MuteSound(type, BgmMute);
            }
            StopBgmSlider = true;
            BgmSlider.value = (BgmMute) ? 0 : soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
            StopBgmSlider = false;
        }
        else //Effect
        {
            if (!isStart)
            {
                EffectMute = !EffectMute;
                soundManager.MuteSound(1, EffectMute);
                soundManager.MuteSound(2, EffectMute);
            }
            StopEffectSlider = true;
            EffectSlider.value = (EffectMute) ? 0 : soundManager.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
            StopEffectSlider = false;
        }
        ChangeSoundImage(type);
    }

    private bool Mute(int type)
    {
        bool mute = type.Equals(0) ? BgmMute : EffectMute;
        return mute;
    }
    public void ChangeSound(int type)
    {
        bool stopSlider = (type.Equals(0)) ? StopBgmSlider : StopEffectSlider;
        if (!stopSlider)
        {
            soundManager.AudioVolume(type, DicSlider[type].value);
            if (Mute(type) && !DicSlider[type].value.Equals(0))
            {
                MuteSound(type);
            }
            ChangeSoundImage(type);
        }
    }
    private void ChangeSoundImage(int type)
    {
        if (type.Equals(0)) //Bgm
        {
            if (BgmMute)
            {
                BgmImage.sprite = BgmSprite[0];
            }
            else
            {
                if (BgmSlider.value.Equals(0))
                {
                    BgmImage.sprite = BgmSprite[0];
                }
                else if (BgmSlider.value < 0.3)
                {
                    BgmImage.sprite = BgmSprite[1];
                }
                else if (BgmSlider.value < 0.6)
                {
                    BgmImage.sprite = BgmSprite[2];
                }
                else
                {
                    BgmImage.sprite = BgmSprite[3];
                }
            }
        }
        else //Effect
        {
            EffectImage.sprite = (EffectSlider.value.Equals(0)) ? EffectSprite[0] : EffectSprite[1];
        }
    }

}

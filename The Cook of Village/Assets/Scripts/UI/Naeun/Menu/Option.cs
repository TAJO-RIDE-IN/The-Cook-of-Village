using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    public Dropdown ResolutionDrop;
    public Toggle WindowToggle;
    public GameObject BgmIcon;
    public Toggle BgmToggle;
    public Slider BgmSlider;
    public GameObject EffectIcon;
    public Toggle EffectToggle;
    public Slider EffectSlider;
    private int optionNum;
    private SoundManager soundManager;

    private Dictionary<int, Toggle> DicToggle = new Dictionary<int, Toggle>();
    private Dictionary<int, Slider> DicSlider = new Dictionary<int, Slider>();

    public void OptionUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
    private void Awake()
    {
        DicToggle.Add(0, BgmToggle);
        DicToggle.Add(1, EffectToggle);
        DicToggle.Add(2, EffectToggle);
        DicSlider.Add(0, BgmSlider);
        DicSlider.Add(1, EffectSlider);
        DicSlider.Add(2, EffectSlider);
    }
    private void OnEnable()
    {
        soundManager = SoundManager.Instance;
        BgmToggle.isOn = soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].mute;
        EffectToggle.isOn = soundManager.EffectMute;
        MuteSound(0);
        MuteSound(1);
        MuteSound(2);
        if(!BgmToggle.isOn)
        {
            BgmSlider.value = soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
        }
        if (!EffectToggle.isOn)
        {
            EffectSlider.value = soundManager.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
        }
        Init();
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
    public void Init()
    {
        ResolutionDrop.options.Clear();
        resolutions.Clear();
        resolutions = GetResolutions();   
        foreach(var res in resolutions.Select((value, index) => (value, index)))
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = res.value.width + "X" + res.value.height;
            ResolutionDrop.options.Add(option);
            if(res.value.width == Screen.width && res.value.height == Screen.height)
            {
                ResolutionDrop.value = res.index;
            }
        }
        WindowToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.Windowed) ? true : false;
        ResolutionDrop.RefreshShownValue();
    }
    public void ChangeScreen()
    {
        int _width = resolutions[ResolutionDrop.value].width;
        int _height = resolutions[ResolutionDrop.value].height;
        Screen.SetResolution(_width, _height, !WindowToggle.isOn);
    }
    private bool StopBgmSlider;
    private bool StopEffectSlider;
    public void MuteSound(int type)
    {
        if(type == 0)
        {
            soundManager.MuteSound(type, BgmToggle.isOn);
            StopBgmSlider = true;
            BgmSlider.value = (BgmToggle.isOn) ? 0 : soundManager.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
            StopBgmSlider = false;
            BgmIcon.SetActive(!BgmToggle.isOn);
        }
        else
        {
            soundManager.EffectMute = EffectToggle.isOn;
            StopEffectSlider = true;
            EffectSlider.value = (EffectToggle.isOn) ? 0 : SoundManager.Instance.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
            StopEffectSlider = false;
            EffectIcon.SetActive(!EffectToggle.isOn);
        }
    }

    public void ChangeSound(int type)
    {
        bool stopSlider = (type == 0) ? StopBgmSlider : StopEffectSlider;
        if(!stopSlider)
        {
            SoundManager.Instance.AudioVolume(type, DicSlider[type].value);
            DicToggle[type].isOn = false;
        }
    }
}

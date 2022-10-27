using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Resolution[] resolutions;
    public Dropdown ResolutionDrop;
    public Toggle WindowToggle;
    public GameObject BgmIcon;
    public Toggle BgmToggle;
    public Slider BgmSlider;
    public GameObject EffectIcon;
    public Toggle EffectToggle;
    public Slider EffectSlider;
    private int optionNum;

    private Dictionary<int, Toggle> DicToggle = new Dictionary<int, Toggle>();
    private Dictionary<int, Slider> DicSlider = new Dictionary<int, Slider>();
    private void Awake()
    {
        DicToggle.Add(0, BgmToggle);
        DicToggle.Add(1, EffectToggle);
        DicToggle.Add(2, EffectToggle);
        DicSlider.Add(0, BgmSlider);
        DicSlider.Add(1, EffectSlider);
        DicSlider.Add(2, EffectSlider);
    }
    private void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        BgmToggle.isOn = SoundManager.Instance.audioSources[(int)SoundData.Type.Bgm].audioSources[0].mute;
        EffectToggle.isOn = SoundManager.Instance.EffectMute;
        MuteSound(0);
        MuteSound(1);
        MuteSound(2);
        if(!BgmToggle.isOn)
        {
            BgmSlider.value = SoundManager.Instance.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
        }
        if (!EffectToggle.isOn)
        {
            EffectSlider.value = SoundManager.Instance.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
        }
    }
    public void Init()
    {
        resolutions = Screen.resolutions;
        ResolutionDrop.options.Clear();
        foreach(Resolution res in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = res.width + "X" + res.height;
            ResolutionDrop.options.Add(option);
            if(res.width == Screen.width && res.height == Screen.height)
            {
                ResolutionDrop.value = optionNum;
            }
            optionNum++;
        }
        ResolutionDrop.RefreshShownValue();
    }
    public void ChangeScreen()
    {
        int _width = resolutions[ResolutionDrop.value].width;
        int _height = resolutions[ResolutionDrop.value].height;
        Screen.SetResolution(_width, _height, WindowToggle.isOn);
    }
    private bool StopBgmSlider;
    private bool StopEffectSlider;
    public void MuteSound(int type)
    {
        if(type == 0)
        {
            SoundManager.Instance.MuteSound(type, BgmToggle.isOn);
            StopBgmSlider = true;
            BgmSlider.value = (BgmToggle.isOn) ? 0 : SoundManager.Instance.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
            StopBgmSlider = false;
            BgmIcon.SetActive(!BgmToggle.isOn);
        }
        else
        {
            SoundManager.Instance.EffectMute = EffectToggle.isOn;
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

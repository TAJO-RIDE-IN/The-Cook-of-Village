using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Resolution[] resolutions;
    public Dropdown ResolutionDrop;
    public Toggle WindowToggle;
    public Slider BgmSlider;
    public Slider EffectSlider;

    private int optionNum;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        BgmSlider.value = SoundManager.Instance.audioSources[(int)SoundData.Type.Bgm].audioSources[0].volume;
        EffectSlider.value = SoundManager.Instance.audioSources[(int)SoundData.Type.Effect].audioSources[0].volume;
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
    public void ChangeBGMSound()
    {
        SoundManager.Instance.AudioVolume(SoundData.Type.Bgm, BgmSlider.value);
    }

    public void ChangeEffectSound()
    {
        SoundManager.Instance.AudioVolume(SoundData.Type.Effect, EffectSlider.value);
        SoundManager.Instance.AudioVolume(SoundData.Type.Effect3D, EffectSlider.value);
    }
}

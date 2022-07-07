using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private Resolution[] resolutions;
    public Dropdown ResolutionDrop;
    public Toggle WindowToggle;

    private void Start()
    {
        Init();
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
        }
        ResolutionDrop.RefreshShownValue();
    }
    public void ChangeScreen()
    {
        int _width = resolutions[ResolutionDrop.value].width;
        int _height = resolutions[ResolutionDrop.value].height;
        Screen.SetResolution(_width, _height, WindowToggle.isOn);
    }
}

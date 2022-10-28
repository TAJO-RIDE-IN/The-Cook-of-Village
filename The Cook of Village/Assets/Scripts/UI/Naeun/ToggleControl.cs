using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ToggleType
{
    public string Type;
    public Toggle[] toggles;
    public ToggleGroup toggleGroup;
}
public class ToggleControl : MonoBehaviour
{
    [SerializeField] public ToggleType[] toggleType;
    public void ResetToggle(int index)
    {
        toggleType[index].toggleGroup.SetAllTogglesOff();
        if (!toggleType[index].toggleGroup.allowSwitchOff)
        {
            toggleType[index].toggles[0].isOn = true;
        }
    }
/*
    public void MenuButtonState(int index, bool state)
    {

    }*/
}

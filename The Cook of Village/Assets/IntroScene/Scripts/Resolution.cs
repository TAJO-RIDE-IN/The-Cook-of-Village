using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Dropdown dropdown;
    public Toggle toggle;
    public void Function_Button()
    {
        if (toggle.isOn)
        {
            if (dropdown.value == 0)
                Screen.SetResolution(1920, 1080, false);

            if (dropdown.value == 1)
                Screen.SetResolution(1600, 900, false);

            if (dropdown.value == 2)
                Screen.SetResolution(1280, 720, false);
        }
        else
        {
            if (dropdown.value == 0)
                Screen.SetResolution(1920, 1080, true);

            if (dropdown.value == 1)
                Screen.SetResolution(1600, 900, true);

            if (dropdown.value == 2)
                Screen.SetResolution(1280, 720, true);
        }
    }
}

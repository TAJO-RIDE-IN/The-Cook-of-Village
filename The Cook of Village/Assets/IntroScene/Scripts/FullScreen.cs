using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using System.Collections.Generic;

public class FullScreen : MonoBehaviour {
 public Toggle toggle;
    public void ChangeScreen(bool isOn)
    {
        int _width = Screen.width;
        int _height = Screen.height;
        if (toggle.isOn)
            Screen.SetResolution(_width, _height, false);
        else
            Screen.SetResolution(_width, _height, true);
    }
}
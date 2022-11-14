using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPositionUI : MonoBehaviour
{
    public int index;

    public GameObject InstallButton;
    public GameObject NoCookTool;
    public Image _image;



    public void DirectSetUp()
    {
        ToolPooling.Instance.toolInstallMode.DirectInstall();
        ToolPooling.Instance.indexToChange = index;
        gameObject.SetActive(false);
    }

    public void ChangeUI(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}

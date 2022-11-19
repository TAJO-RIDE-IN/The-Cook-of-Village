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

    private ToolPooling _toolPooling;


    private void Start()
    {
        _toolPooling = ToolPooling.Instance;
    }

    public void DirectSetUp()
    {
        _toolPooling.toolInstallMode._cookingCharacter.isSpace = false;
        _toolPooling.toolInstallMode.DirectInstall();
        _toolPooling.indexToChange = index;
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

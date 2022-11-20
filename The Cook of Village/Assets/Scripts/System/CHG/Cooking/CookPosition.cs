using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPosition : MonoBehaviour
{
    public int index;

    public GameObject CookPositionUI;
    private ToolPooling _toolPooling;


    private void Start()
    {
        _toolPooling = ToolPooling.Instance;
        CookPositionUI.transform.localScale = Vector2.zero;
        CookPositionUI.SetActive(true);
    }

    public void DirectSetUp()
    {
        _toolPooling.toolInstallMode._cookingCharacter.isSpace = false;
        _toolPooling.toolInstallMode.DirectInstall();
        _toolPooling.indexToChange = index;
        gameObject.SetActive(false);
    }

    public void OpenUI(float time)
    {
        CookPositionUI.LeanScale(Vector3.one, time).setEaseOutElastic();
    }

    public void CloseUI()
    {
        CookPositionUI.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }
}

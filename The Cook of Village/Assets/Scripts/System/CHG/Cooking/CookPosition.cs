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
    

    private int fadeCount = 5;
    


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
    }

    public void OpenUI(float time)
    {
        CookPositionUI.LeanScale(Vector3.one, time).setEaseOutElastic();
        if (index == 6)//오븐이면 깜빡이는것
        {
            
        }
    }

    public void CloseUI(float time)
    {
        //if(ChefInventory.)
        CookPositionUI.LeanScale(Vector2.zero, time).setEaseInBack().setOnComplete(() =>
            ChefInventory.Instance._cookingCharacter._cookingTool.OpenUI(0.5f));
        
    }
}

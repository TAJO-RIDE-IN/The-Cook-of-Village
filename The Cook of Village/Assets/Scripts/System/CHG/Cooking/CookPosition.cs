using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPosition : MonoBehaviour
{
    public int index;

    public GameObject CookPositionUI;
    public Material transMaterial;
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
        gameObject.SetActive(false);
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
        CookPositionUI.LeanScale(Vector2.zero, time).setEaseInBack();
    }
    
    public IEnumerator MaterialFadeOut(Material material,float fadeOutValue)
    {
        for (int i = 0; i < fadeCount; i++)
        {
            if (i % 2 == 0)
            {
                while (material.color.a > fadeOutValue)
                {
                    material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - (Time.deltaTime / 3.0f));
                    yield return null;
                }
            }
        }
        
        
    }
}

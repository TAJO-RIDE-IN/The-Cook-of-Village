using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFade : MonoBehaviour
{
    public Material transMaterial;
    public GameObject ovenObject;
    
    public int fadeCount;

    private void Start()
    {
        
    }
    //private void 

    public IEnumerator MaterialFadeOut(Material material,float fadeOutValue)
    {
        while (fadeCount < 5)
        {
            
        }
        for (int i = 0; i < fadeCount; i++)
        {
            //LeanTween.color(, )
            if (i % 2 == 0)
            {
                while (material.color.a > fadeOutValue)
                {
                    material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - (Time.deltaTime / 3.0f));
                    yield return null;
                }
            }
            else
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

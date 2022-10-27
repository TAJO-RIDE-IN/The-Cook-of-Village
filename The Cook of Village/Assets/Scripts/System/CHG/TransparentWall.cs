using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public Material wallMaterial;
    public Material doorMaterial;
    public bool isIn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (isIn)
            {
                StartCoroutine(MaterialFadeOut(wallMaterial, 50));
                StartCoroutine(MaterialFadeOut(doorMaterial, 50));
                return;
            }

            StartCoroutine(MaterialFadeIn(wallMaterial, 255));
            StartCoroutine(MaterialFadeIn(doorMaterial, 255));
        }
    }
    public IEnumerator MaterialFadeOut(Material material,float fadeOutValue)
    {
        while (material.color.a > fadeOutValue)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        
    }
    public IEnumerator MaterialFadeIn(Material material,float fadeInValue)
    {
        while (material.color.a < fadeInValue)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }
        
    }
}

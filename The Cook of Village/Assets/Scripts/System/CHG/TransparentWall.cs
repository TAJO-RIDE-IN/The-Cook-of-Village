using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public GameObject wallObject;
    public Material[] materialToChange;
    
    public Material[] usedMaterial;

    private Material abc;
    
    [SerializeField, Range(0, 2f)] private double time = 1f;
    [SerializeField, Range(0, 1f)] private float minimalOpacity = 0.1f;
    
    private float step
    {
        get
        {
            if (time > 0.1f) return 0.01f;
            else return 0.09f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //abc = wallObject.GetComponent<MeshRenderer>().materials[0];
            Debug.Log(wallObject.GetComponent<MeshRenderer>().materials[0]);
            //wallObject.GetComponent<MeshRenderer>().materials[0] = usedMaterial[0];
            StartCoroutine(FadeIn(wallObject.GetComponent<MeshRenderer>().materials[0]));
            //LeanTween.color(wallObject, new Color(1, 1, 1, 0), 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LeanTween.color(wallObject, Color.white, 1).setOnComplete(() =>
                wallObject.GetComponent<MeshRenderer>().materials[0] = materialToChange[0]);
        }
    }
    private IEnumerator FadeIn(Material mat)
    {

        float steps = (1 - minimalOpacity) / step;
        double timeStep = time / steps;

        float opacity = minimalOpacity;
        while (opacity < 1f)
        {
            opacity += step;
            Color c1 = mat.color;
            c1.a = opacity;
            mat.color = c1;

            yield return new WaitForSeconds((float)timeStep);
        }

        Color c = mat.color;
        c.a = opacity;
        mat.color = c;

        //StandardShaderUtils.ChangeTransparency(mat, false);

    }
}

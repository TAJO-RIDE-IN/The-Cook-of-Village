using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public GameObject wallObject;
    
    public Material[] materialToChange;
    
    public Material[] originalMaterial;
    public Material[] transMaterial;

    private Material abc;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ChangeTrans();
            LeanTween.color(wallObject, Color.clear, 1);
        }
    }

    private void ChangeTrans()
    {
        materialToChange = wallObject.transform.GetComponent<MeshRenderer>().materials;
        materialToChange[0] = transMaterial[0];
        materialToChange[1] = transMaterial[1];
        wallObject.transform.GetComponent<MeshRenderer>().materials = materialToChange;
    }
    private void ChangeOriginal()
    {
        materialToChange = wallObject.transform.GetComponent<MeshRenderer>().materials;
        materialToChange[0] = originalMaterial[0];
        materialToChange[1] = originalMaterial[1];
        wallObject.transform.GetComponent<MeshRenderer>().materials = materialToChange;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LeanTween.color(wallObject, Color.white, 1).setOnComplete(() => ChangeOriginal());
        }
    }
}

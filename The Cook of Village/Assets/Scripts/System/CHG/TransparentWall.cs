using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public GameObject wallObject;
    public Material[] materialToChange;
    
    public Material[] usedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(wallObject.GetComponent<MeshRenderer>().materials[0]);
            wallObject.GetComponent<MeshRenderer>().materials[0] = usedMaterial[0];
            LeanTween.color(wallObject, new Color(1, 1, 1, 0), 0.5f);
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransIn : MonoBehaviour
{
    public bool isIn;
    public GameObject transObject;

    public GameObject[] transObjects;
    public GameObject[] transRevert;
    public Material[] transMaterials;

    private Material[] _materials;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isIn)
            {
                Change();
            }
        }
    }

    public void Change()
    {
        ChangeMaterial();
        LeanTween.color(transObject, Color.clear, 0.5f);
        isIn = true;
    }

    public void ChangeMaterial()
    {
        for (int i = 0; i < transObjects.Length; i++)
        {
            transObjects[i].transform.GetComponent<MeshRenderer>().material = transMaterials[0];
        }

        for (int t = 0; t < transRevert.Length; t++)
        {
            _materials = transRevert[t].transform.GetComponent<MeshRenderer>().materials;
            _materials[0] = transMaterials[1];
            _materials[1] = transMaterials[0];
            transRevert[t].transform.GetComponent<MeshRenderer>().materials = _materials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}

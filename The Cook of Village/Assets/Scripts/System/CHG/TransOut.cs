using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransOut : MonoBehaviour
{
    
    public TransIn transIn;
    public GameObject transObject;

    
    public GameObject[] transObjects;
    public GameObject[] transRevert;
    
    public Material[] noTransMaterials;
    
    private Material[] _materials;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transIn.isIn)
            {
                ChangeMaterial();
                LeanTween.color(transObject, new Color(1, 1, 1, 1f), 0.5f);
                transIn.isIn = false;
            }
        }
    }
    
    private void ChangeMaterial()
    {
        for (int i = 0; i < transObjects.Length; i++)
        {
            transObjects[i].transform.GetComponent<MeshRenderer>().material = noTransMaterials[0];
        }

        for (int t = 0; t < transRevert.Length; t++)
        {
            _materials = transRevert[t].transform.GetComponent<MeshRenderer>().materials;
            _materials[0] = noTransMaterials[1];
            _materials[1] = noTransMaterials[0];
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

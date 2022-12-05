using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransIn : MonoBehaviour
{
    public bool isIn;
    public TransOut transOut;
    
    public GameObject transObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isIn)
            {
                
                LeanTween.color(transObject, Color.white, 0.5f);
                isIn = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}

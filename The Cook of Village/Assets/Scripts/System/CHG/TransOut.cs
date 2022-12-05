using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransOut : MonoBehaviour
{
    public bool isOut;
    
    public TransIn transIn;

    public GameObject transObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transIn.isIn)
            {
                LeanTween.color(transObject, Color.clear, 0.5f);
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

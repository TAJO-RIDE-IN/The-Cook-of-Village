using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterativeObject : MonoBehaviour
{
    public GameObject smallUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            smallUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            smallUI.SetActive(false);
        }
    }
    
}

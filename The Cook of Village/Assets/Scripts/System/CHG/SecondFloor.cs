using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloor : MonoBehaviour
{
    public CameraLayer cameraLayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!cameraLayer.IsSecondFloor)
            {
                cameraLayer.IsSecondFloor = true;
            }
            else
            {
                cameraLayer.IsSecondFloor = false;
            }
        }
    }
}

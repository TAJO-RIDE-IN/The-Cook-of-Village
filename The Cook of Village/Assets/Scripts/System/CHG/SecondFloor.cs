using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloor : MonoBehaviour
{
    public bool isGoUp;
    public bool isSecond;
    public CameraLayer cameraLayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShowOrNot();
        }
    }

    private void ShowOrNot()
    {
        if (isGoUp)
        {
            if (!cameraLayer.IsSecondFloor)
            {
                cameraLayer.IsSecondFloor = true;
            }
            
        }
        else//내려갈 때
        {
            if (cameraLayer.IsSecondFloor)
            {
                cameraLayer.IsSecondFloor = false;
            }
            
        }
    }
}

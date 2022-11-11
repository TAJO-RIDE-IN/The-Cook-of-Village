using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera3DZoom : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    
    public float distance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsUI)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                distance = Input.GetAxis("Mouse ScrollWheel");
                
                /*cinemachine.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_ScreenX += distance;
                cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX += distance;
                cinemachine.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_ScreenX += distance;*/

            }
            else
            {
            }
        }
    }
}

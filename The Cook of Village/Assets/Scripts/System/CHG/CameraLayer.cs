using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraLayer : MonoBehaviour
{
    // Start is called before the first frame update]
    private Camera _camera;
    private bool isSecondFloor;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (!isSecondFloor)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _camera.cullingMask |= 1 << 7;
                isSecondFloor = true;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _camera.cullingMask = ~(1 << 7);
            isSecondFloor = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLayer : MonoBehaviour
{
    // Start is called before the first frame update]
    private Camera _camera;
    private bool isSecondFloor;
    public GameObject secondInstallPlace;

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
                secondInstallPlace.layer = 9;
                //2층 바닥 레이어 InstallPlace로 변경해주기, 위에 설치 가능한 것들 레이어 Install
                _camera.cullingMask |= 1 << LayerMask.NameToLayer("SecondFloor");
                isSecondFloor = true;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            secondInstallPlace.layer = 7;
            _camera.cullingMask = ~(1 << LayerMask.NameToLayer("SecondFloor"));
            isSecondFloor = false;
        }
        
    }
}

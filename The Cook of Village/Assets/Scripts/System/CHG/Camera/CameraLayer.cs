using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLayer : MonoBehaviour
{
    // Start is called before the first frame update]
    public SecondFloor secondFloor;
    public Camera _camera;
    private bool isSecondFloor = false;
    public TransIn transIn;

    public bool IsSecondFloor
    {
        get
        {
            return isSecondFloor;
        }
        set
        {
            isSecondFloor = value;
            secondFloor.isSecond = value;
            if (value)
            {
                cameraMovement.outerUp = 13;
                cameraMovement.outerDown = -11;
                SecondFloor();
            }
            else
            {
                cameraMovement.outerUp = 9;
                cameraMovement.outerDown = -9;
                NoSecondFloor();
            }
        }
    }
    public GameObject secondInstallPlace;
    public GameObject secondInstallWall;
    public CameraMovement cameraMovement;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isSecondFloor)
            {
                IsSecondFloor = true;
                return;
            }
            else
            {
                IsSecondFloor = false;
            }
        }
    }

    public void SecondFloor()
    {
        Debug.Log("2층으로 변경");
        for (int i = 0; i < FurniturePooling.Instance.secondObjects.Count; i++)
        {
            FurniturePooling.Instance.secondObjects[i].layer = 10;
        }
        secondInstallPlace.layer = 9;
        foreach(Transform child in secondInstallPlace.transform)
        {
            child.gameObject.layer = 9;
        }
        secondInstallWall.layer = 11;
        foreach(Transform child in secondInstallWall.transform)
        {
            child.gameObject.layer = 11;
        }
        //2층 바닥 레이어 InstallPlace로 변경해주기, 위에 설치 가능한 것들 레이어 Install
        _camera.cullingMask |= 1 << LayerMask.NameToLayer("SecondFloor");
    }
    public void NoSecondFloor()
    {
        Debug.Log("1층으로 변경");
        for (int i = 0; i < FurniturePooling.Instance.secondObjects.Count; i++)
        {
            FurniturePooling.Instance.secondObjects[i].layer = 7;
        }

        secondInstallPlace.layer = 7;
        foreach(Transform child in secondInstallPlace.transform)
        {
            child.gameObject.layer = 7;
        }
        secondInstallWall.layer = 7;
        foreach(Transform child in secondInstallWall.transform)
        {
            child.gameObject.layer = 7;
        }
        _camera.cullingMask = ~(1 << LayerMask.NameToLayer("SecondFloor"));
    }

    private void SecondFloorCamera()
    {
        
    }
    private void FirstFloorCamera()
    {
        
    }
}

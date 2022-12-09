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

    Transform[] allChildrenPlace;
    Transform[] allChildrenWall;

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
                //cameraMovement.outerUp = 13;
                //cameraMovement.outerDown = -11;
                SecondFloor();
            }
            else
            {
                //cameraMovement.outerUp = 9;
                //cameraMovement.outerDown = -9;
                NoSecondFloor();
            }
        }
    }
    public GameObject secondInstallPlace;
    public GameObject secondInstallWall;
    public CameraMovement cameraMovement;

    private void Start()
    {
        allChildrenPlace = secondInstallPlace.GetComponentsInChildren<Transform>();
        allChildrenWall = secondInstallWall.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
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
        for (int i = 0; i < FurniturePooling.Instance.secondObjects.Count; i++)
        {
            FurniturePooling.Instance.secondObjects[i].layer = 10;
        }
        secondInstallPlace.layer = 9;
        
        foreach(Transform child in allChildrenPlace)
        {
            child.gameObject.layer = 9;
        }
        
        secondInstallWall.layer = 11;
        foreach (Transform child in allChildrenWall)
        {
            child.gameObject.layer = 11;
        }
        _camera.cullingMask |= 1 << LayerMask.NameToLayer("SecondFloor");
    }
    public void NoSecondFloor()
    {
        for (int i = 0; i < FurniturePooling.Instance.secondObjects.Count; i++)
        {
            FurniturePooling.Instance.secondObjects[i].layer = 7;
            foreach (Transform child in FurniturePooling.Instance.secondObjects[i].transform)
            {
                child.gameObject.layer = 7;
            }
        }

        secondInstallPlace.layer = 7;
        foreach(Transform child in allChildrenPlace)
        {
            child.gameObject.layer = 7;
        }
        secondInstallWall.layer = 7;
        foreach(Transform child in allChildrenWall)
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

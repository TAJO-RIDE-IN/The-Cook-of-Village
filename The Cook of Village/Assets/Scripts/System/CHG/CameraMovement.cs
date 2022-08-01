using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed;
    public float zoomSpeed;
    public float outerLeft;
    public float outerRight;
    public float outerDown;
    public float outerUp;
    public float distance;

    public GameObject cameraPosition;
    public GameObject flatCamera;

    public float zoomValue;
    public CinemachineFreeLook cinemachine;
    
    private Vector3 TargetPosition;
    private Vector3 upDirection;
    private Vector3 rightDirection;
    private float preAngle;

    private bool isAngle = true;
    
    private void Start()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        upDirection = Vector3.forward;
        rightDirection = Vector3.right;//이거
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(1))
        {
            isAngle = false;
            cinemachine.m_XAxis.m_MaxSpeed = 300;
            
            cameraPosition.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
            cinemachine.m_XAxis.Value += Input.GetAxis("Mouse X");
            cameraPosition.transform.rotation = flatCamera.transform.rotation;
        }
        else
        {
            if (!isAngle)
            {
                upDirection = Quaternion.Euler(0, cinemachine.m_XAxis.Value - preAngle, 0) * upDirection;
                preAngle = cinemachine.m_XAxis.Value;
                isAngle = true;
            }
            cinemachine.m_XAxis.m_MaxSpeed = 0;
        }
        distance = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomValue < 3)
            {
                cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                zoomValue += distance;
                outerDown += Mathf.Abs(distance);
            }

        }
        else
        {
            if (zoomValue > -5)
            {
                cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                zoomValue += distance;
                outerDown -= Mathf.Abs(distance);
            }
        }
    }
    void LateUpdate()
    {
        
        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log(Vector3.forward);
            //Debug.Log(upDirection);
            
            //new Vector3()
            if (cameraPosition.transform.position.x >= outerLeft && cameraPosition.transform.position.x <= outerRight &&
                cameraPosition.transform.position.z >= outerDown && cameraPosition.transform.position.z <= outerUp)
            {
                cameraPosition.transform.Translate(-upDirection * Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime
                    , Space.World);
                cameraPosition.transform.Translate(-Vector3.right * Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime,
                    Space.Self);
            }
            
            CameraPosition();
        }

    }

    public void CameraPosition()
    {
        if (cameraPosition.transform.position.x < outerLeft)
        {
            TargetPosition = new Vector3(outerLeft, cameraPosition.transform.localPosition.y,
                cameraPosition.transform.localPosition.z);
            StartCoroutine(SetCameraPosition());
        }
        if (cameraPosition.transform.position.x > outerRight)
        {
            TargetPosition = new Vector3(outerRight, cameraPosition.transform.localPosition.y,
                cameraPosition.transform.localPosition.z);
            StartCoroutine(SetCameraPosition());
        }
        if (cameraPosition.transform.position.z < outerDown)
        {
            TargetPosition = new Vector3(cameraPosition.transform.localPosition.x, cameraPosition.transform.localPosition.y,
                outerDown);
            StartCoroutine(SetCameraPosition());
        }
        if(cameraPosition.transform.position.z > outerUp)
        {
            TargetPosition = new Vector3(cameraPosition.transform.localPosition.x, cameraPosition.transform.localPosition.y,
                outerUp);
            StartCoroutine(SetCameraPosition());
        }
    }
    public IEnumerator SetCameraPosition()
    {
        //yield return new WaitForSeconds(1f);
        
        while (true)
        {
            //Button.transform.position = Vector3.Lerp(Button.transform.position, new Vector3(Button.transform.position.x, ButtonTarget.transform.position.y, Button.transform.position.z), Time.deltaTime * ButtonSpeed);
            cameraPosition.transform.position = Vector3.MoveTowards(cameraPosition.transform.position, TargetPosition,
                Time.deltaTime);

            if (Mathf.Approximately(TargetPosition.x, cameraPosition.transform.position.x) &&
                Mathf.Approximately(TargetPosition.z, cameraPosition.transform.position.z))
            {
                break;
            }
        }
        yield return null;
    }
}

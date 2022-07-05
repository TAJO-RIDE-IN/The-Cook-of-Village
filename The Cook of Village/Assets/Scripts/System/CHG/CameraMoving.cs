using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float dragSpeed;
    public float zoomSpeed;
    public float outerLeft;
    public float outerRight;
    public float outerDown;
    public float outerUp;
    public float distance;
    
    public Camera flatCamera;

    public float zoomValue;

    private Vector3 TargetPosition;
    
    private void Start()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void Update()
    {
        //Debug.Log(flatCamera.transform.localPosition);
        //Debug.Log(zoomValue);
        distance = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //Debug.Log(distance);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            if (zoomValue < 2)
            {
                flatCamera.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                zoomValue += distance;
                outerDown += Mathf.Abs(distance);
            }

            //flatCamera.transform.position = Vector3.Lerp(flatCamera.transform.position, flatCamera.transform.forward*distance, Time.deltaTime);
        }
        else
        {
            if (zoomValue > -2)
            {
                flatCamera.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime,
                    Space.World);
                zoomValue += distance;
                outerDown -= Mathf.Abs(distance);
            }
        }
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            var newPosition = new Vector3();
            newPosition.x = Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime;
            newPosition.z = Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime;

            if (flatCamera.transform.position.x >= outerLeft && flatCamera.transform.position.x <= outerRight &&
                flatCamera.transform.position.z >= outerDown && flatCamera.transform.position.z <= outerUp)
            {
                
                flatCamera.transform.Translate(-newPosition, Space.World);
            }
            Debug.Log("카메라움직임 끝");
            


        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraPosition();
        }
    }

    public void CameraPosition()
    {
        if (flatCamera.transform.position.x < outerLeft)
        {
            TargetPosition = new Vector3(outerLeft, flatCamera.transform.localPosition.y,
                flatCamera.transform.localPosition.z);
            StartCoroutine(CameraMovement());
        }
        if (flatCamera.transform.position.x > outerRight)
        {
            TargetPosition = new Vector3(outerRight, flatCamera.transform.localPosition.y,
                flatCamera.transform.localPosition.z);
            StartCoroutine(CameraMovement());
        }
        if (flatCamera.transform.position.z < outerDown)
        {
            TargetPosition = new Vector3(flatCamera.transform.localPosition.x, flatCamera.transform.localPosition.y,
                outerDown);
            StartCoroutine(CameraMovement());
        }
        if(flatCamera.transform.position.z > outerUp)
        {
            TargetPosition = new Vector3(flatCamera.transform.localPosition.x, flatCamera.transform.localPosition.y,
                outerUp);
            StartCoroutine(CameraMovement());
        }
    }
    public IEnumerator CameraMovement()
    {
        //yield return new WaitForSeconds(1f);
        
        while (true)
        {
            //Button.transform.position = Vector3.Lerp(Button.transform.position, new Vector3(Button.transform.position.x, ButtonTarget.transform.position.y, Button.transform.position.z), Time.deltaTime * ButtonSpeed);
            flatCamera.transform.position = Vector3.MoveTowards(flatCamera.transform.position, TargetPosition,
                Time.deltaTime);

            if (Mathf.Approximately(TargetPosition.x, flatCamera.transform.position.x) &&
                Mathf.Approximately(TargetPosition.z, flatCamera.transform.position.z))
            {
                break;
            }
        }
        yield return null;
    }
}

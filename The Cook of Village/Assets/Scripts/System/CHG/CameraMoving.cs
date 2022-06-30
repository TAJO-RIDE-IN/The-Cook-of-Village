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
    
    public Camera flatCamera;

    public float zoomValue;
    
    private void Start()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void Update()
    {
        Debug.Log(flatCamera.transform.localPosition);
        float distance = Input.GetAxis("Mouse ScrollWheel") * 1 * zoomSpeed;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            if (zoomValue < 20)
            {
                flatCamera.transform.Translate(flatCamera.transform.forward * distance * Time.deltaTime * zoomSpeed, Space.World);
                zoomValue += distance;
            }
            
            //flatCamera.transform.position = Vector3.Lerp(flatCamera.transform.position, flatCamera.transform.forward*distance, Time.deltaTime);
        }
        else
        {
            if (zoomValue > -20)
            {
                flatCamera.transform.Translate(flatCamera.transform.forward * distance * Time.deltaTime * zoomSpeed, Space.World);
                zoomValue += distance;
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

            if (flatCamera.transform.position.x > outerLeft && flatCamera.transform.position.x < outerRight &&
                flatCamera.transform.position.z > outerDown && flatCamera.transform.position.z < outerUp)
            {
                flatCamera.transform.Translate(-newPosition, Space.World);
            }

            if (flatCamera.transform.position.x < outerLeft || flatCamera.transform.position.x > outerRight ||
                flatCamera.transform.position.z < outerDown || flatCamera.transform.position.z > outerUp)
            {
                flatCamera.transform.Translate(newPosition, Space.World);
            }
        }
    }
}

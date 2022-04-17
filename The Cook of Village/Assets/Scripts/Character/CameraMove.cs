using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float dragSpeed;
    public float zoomSpeed;
    public float outerLeft;
    public float outerRight;
    public float outerDown;
    public float outerUp;
    
    public Camera flatCamera;
    
    private void Start()
    {
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void Update()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (flatCamera.fieldOfView >=30)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                flatCamera.fieldOfView += distance;
            }
        }
        if (flatCamera.fieldOfView <= 70)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                flatCamera.fieldOfView += distance;
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

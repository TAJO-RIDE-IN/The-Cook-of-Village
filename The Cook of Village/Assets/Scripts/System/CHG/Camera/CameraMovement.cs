using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[Serializable]
public class CinemachineName
{
    public String name;
    public CinemachineFreeLook cinemachine;
}
public class CameraMovement : MonoBehaviour
{
    public float dragSpeed;
    public float zoomSpeed;
    public float outerLeft;
    public float outerRight;
    public float outerDown;
    public float outerUp;
    public float distance;
    
    
    private float outerLeftOriginal;
    private float outerRightOriginal;
    private float outerDownOriginal;
    private float outerUpOriginal;

    public GameObject cameraPosition;
    public GameObject flatCamera;

    public Vector3 helloWorld;
    public float zoomValue;
    public CinemachineName[] cinemachineName; 
    private GameManager _gameManager;
    
    private Vector3 TargetPosition;
    private Vector3 upDirection;
    private float preAngle;
    private float preOuterDown;
    public Transform characterTrans;
    
    
    private float rotationY;
    private float characterY;
    private bool isAngle = true;
    private bool isLocked;
    private ThirdPersonMovement character;
   
    
    private void Start()
    {
        outerLeftOriginal = outerLeft;
        outerRightOriginal = outerRight;
        outerDownOriginal = outerDown;
        outerUpOriginal = outerUp;
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        upDirection = Vector3.forward;
        _gameManager = GameManager.Instance;
        characterTrans = GameObject.FindWithTag("Player").transform;
        character = GameObject.FindWithTag("Player").GetComponent<ThirdPersonMovement>();

    }

    private void FixedUpdate()
    {
        helloWorld = character.transform.position;
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!isLocked)//캐릭터로 이동
            {
                cameraPosition.transform.position = character.transform.position;
                zoomValue = 0;
                character.isLocked = true;
                isLocked = true;
            }
            else//카메라 포지션으로 이동
            {
                outerLeft = outerLeftOriginal;
                outerRight = outerRightOriginal;
                outerDown = outerDownOriginal;
                outerUp = outerUpOriginal;
                character.isLocked = false;
                isLocked = false;
            }
        }
        if (!_gameManager.IsUI)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(1))
            {
                isAngle = false;
                cinemachineName[0].cinemachine.m_XAxis.m_MaxSpeed = 300;
                //cameraPosition.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
                cinemachineName[0].cinemachine.m_XAxis.Value += Input.GetAxis("Mouse X");
                cameraPosition.transform.rotation = Quaternion.Euler(0, flatCamera.transform.eulerAngles.y, 0);
                //Debug.Log(flatCamera.transform.rotation.y);
            }
            else
            {
                if (!isAngle)
                {
                    upDirection = Quaternion.Euler(0, cinemachineName[0].cinemachine.m_XAxis.Value - preAngle, 0) *
                                  upDirection; //이게 upDirection을 이 회전각도에 따라서 바꿔주는것
                    preAngle = cinemachineName[0].cinemachine.m_XAxis.Value;
                    isAngle = true;
                }

                cinemachineName[0].cinemachine.m_XAxis.m_MaxSpeed = 0;
            }
        }

        if (!_gameManager.IsUI)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                preOuterDown = cameraPosition.transform.position.z;
                distance = Input.GetAxis("Mouse ScrollWheel");
                if (zoomValue < 2)
                {
                    cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                    zoomValue += distance;
                    //outerDown += Mathf.Abs(distance);
                    outerDown += Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                    outerUp += Math.Abs(preOuterDown - cameraPosition.transform.position.z);

                }

            }
            else
            {
                preOuterDown = cameraPosition.transform.position.z;
                distance = Input.GetAxis("Mouse ScrollWheel");
                if (zoomValue > -2)
                {
                    cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                    zoomValue += distance;
                    //outerDown -= Mathf.Abs(distance);
                    outerDown -= Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                    outerUp -= Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                }
            }
        }
        
    }
    void LateUpdate()
    {
        if (!character.isLocked)
        {
            if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftControl))
            {
                //Debug.Log(Vector3.forward);
                //Debug.Log(upDirection);
            
                //new Vector3()
                if (cameraPosition.transform.position.x >= outerLeft && cameraPosition.transform.position.x <= outerRight &&
                    cameraPosition.transform.position.z >= outerDown && cameraPosition.transform.position.z <= outerUp)
                {
                    cameraPosition.transform.Translate(-cameraPosition.transform.forward * Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime
                        , Space.World);
                    cameraPosition.transform.Translate(-Vector3.right * Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime,
                        Space.Self);
                }
            
                CameraPosition();
            }
        }
    }

    private void CameraPosition()
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
    private IEnumerator SetCameraPosition()
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

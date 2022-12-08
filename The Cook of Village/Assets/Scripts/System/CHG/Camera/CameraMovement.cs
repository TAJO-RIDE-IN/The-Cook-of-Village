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
    public float outerLeft;
    public float outerRight;
    public float outerDown;
    public float outerUp;
    public float distance;
    public float zoomSpeed;
    public float zoomValue;
    public float lockedZoomSpeed;
    public float lockedZoomValue;
    
    
    public float outerLeftOriginal;
    public float outerRightOriginal;
    public float outerDownOriginal;
    public float outerUpOriginal;

    public GameObject cameraPosition;
    public GameObject flatCamera;

    
    public CinemachineFreeLook cinemachine;
    private GameManager _gameManager;
    
    private Vector3 TargetPosition;
    private Vector3 upDirection;
    private float preAngle;
    private float preOuterDown;
    
    
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
        character = GameObject.FindWithTag("Player").GetComponent<ThirdPersonMovement>();

    }

    private void Update()
    {
        if (!_gameManager.IsUI)
        {
            if (isLocked)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (lockedZoomValue <= 0.2f)
                    {
                        lockedZoomValue += 0.1f;
                        distance = Input.GetAxis("Mouse ScrollWheel") * lockedZoomSpeed;
                        ValueChange();
                    }
            
                }
                else if(Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (lockedZoomValue > -0.7f)
                    {
                        distance = Input.GetAxis("Mouse ScrollWheel") * lockedZoomSpeed;
                        lockedZoomValue -= 0.1f;
                        ValueChange();
                    }
            
                }
                
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (!isLocked)//ìºë¦­?°ë¡œ ?´ë™
                {
                    character.IsLocked = true;
                    isLocked = true;
                    return;
                }
                else//ì¹´ë©”???¬ì??˜ìœ¼ë¡??´ë™
                {

                    character.IsLocked = false;
                    isLocked = false;
                    return;
                }
            }
            if (!isLocked)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    preOuterDown = cameraPosition.transform.position.z;
                    distance = Input.GetAxis("Mouse ScrollWheel");
                    if (cameraPosition.transform.position.y > -2)
                    {
                        cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                        zoomValue += distance;
                        //outerDown += Mathf.Abs(distance);
                        //outerDown += Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                        //outerUp += Math.Abs(preOuterDown - cameraPosition.transform.position.z);

                    }

                }
                else if(Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    preOuterDown = cameraPosition.transform.position.z;
                    distance = Input.GetAxis("Mouse ScrollWheel");
                    //?„ë¡œ
                    if (cameraPosition.transform.position.y < 8)
                    {
                        cameraPosition.transform.Translate(flatCamera.transform.forward * distance * zoomSpeed * Time.deltaTime, Space.World);
                        zoomValue += distance;
                        //outerDown -= Mathf.Abs(distance);
                        //outerDown -= Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                        //outerUp -= Math.Abs(preOuterDown - cameraPosition.transform.position.z);
                    }
                }
            }
            
        }
    }

    private void ValueChange()
    {
        cinemachine.m_Orbits[0].m_Height -= distance;
        cinemachine.m_Orbits[1].m_Height -= distance;
        cinemachine.m_Orbits[2].m_Height -= distance;
        cinemachine.m_Orbits[0].m_Radius -= distance;
        cinemachine.m_Orbits[1].m_Radius -= distance;
        cinemachine.m_Orbits[2].m_Radius -= distance;
    }

    private void FixedUpdate()
    {
        
        if (!_gameManager.IsUI)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(1))
            {
                isAngle = false;
                cinemachine.m_XAxis.m_MaxSpeed = 300;
                //cameraPosition.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
                cinemachine.m_XAxis.Value += Input.GetAxis("Mouse X");
                cameraPosition.transform.rotation = Quaternion.Euler(0, flatCamera.transform.eulerAngles.y, 0);
                return;
                //Debug.Log(flatCamera.transform.rotation.y);
            }
            else
            {
                if (!isAngle)
                {
                    upDirection = Quaternion.Euler(0, cinemachine.m_XAxis.Value - preAngle, 0) *
                                  upDirection; //?´ê²Œ upDirection?????Œì „ê°ë„???°ë¼??ë°”ê¿”ì£¼ëŠ”ê²?
                    preAngle = cinemachine.m_XAxis.Value;
                    isAngle = true;
                    return;
                }
                cinemachine.m_XAxis.m_MaxSpeed = 0;
            }
            
            
        }

    }
    void LateUpdate()
    {
        if (!character.IsLocked)
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

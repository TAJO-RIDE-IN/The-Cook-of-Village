using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonGravity : MonoBehaviour, IObserver<GameData>
{
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            animator.SetBool("isSleep",true);
        }
    }
    public void AddObserver(IGameDataOb o)
    {
        o.AddSleepObserver(this);
    }
    public CharacterController controller;
    public Transform cam;

    public float speed = 3;
    public float OriginSpeed = 3;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    private float zoomSpeed = 2;
    public float zoomValue;
    private float distance;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public Animator animator;
    public Animation animation;
    public CinemachineFreeLook cinemachine;

    private GameManager _gameManager;
    private SoundManager _soundManager;

    private bool isCanWalk = true;
    private bool isWalkSound = false;
    private bool isShift;
    private bool isShiftUp;
    private float pitch = 1;

    public float Pitch
    {
        get
        {
            return pitch;
        }
        set
        {
            pitch = value;
            isWalkSound = false;
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _soundManager = SoundManager.Instance;
        AddObserver(GameData.Instance);
    }

    private void Update()
    {
        if (!_gameManager.IsUI)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (zoomValue <= 2f)
                {
                    zoomValue += 0.1f;
                    distance = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                    ValueChange();
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (zoomValue > -2f)
                {
                    distance = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                    zoomValue -= 0.1f;
                    ValueChange();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("재생");
            animator.SetBool("isSleep" ,true);
        }
    }

    private void ValueChange()
    {
        cinemachine.m_Orbits[0].m_Height -= distance;
        cinemachine.m_Orbits[1].m_Height -= distance;
        //cinemachine.m_Orbits[2].m_Height -= distance;
        cinemachine.m_Orbits[0].m_Radius -= distance;
        cinemachine.m_Orbits[1].m_Radius -= distance;
        cinemachine.m_Orbits[2].m_Radius -= distance;
    }
    

    void FixedUpdate()
    {
        if (_gameManager.IsUI)
        {
            StopMovingXYAxis();
        }
        else
        {
            MovingXYAxis();
        }
        if (isCanWalk)
        {
            
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isShift)
                {
                    speed = OriginSpeed * 1.5f;
                    animator.SetBool("isRun",true);
                    Pitch = 1.3f;
                    isShift = true;
                    isShiftUp = false;
                }
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                
                if (!isShiftUp)
                {
                    Pitch = 1f;
                    speed = OriginSpeed;
                    animator.SetBool("isRun", false);
                    isShift = false;
                    isShiftUp = true;
                }
                
                
            }
            //gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            //walk
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                if (!isWalkSound)
                {
                    _soundManager.PlayEffect3D(_soundManager._audioClips["CookWalk2"], gameObject, true, pitch);
                    Debug.Log(pitch);
                    isWalkSound = true;
                }
                
                animator.SetBool("isWalk",true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isWalk",false);
                _soundManager.StopEffect3D(gameObject);
                isWalkSound = false;
            }
        }
    }
    public void WhenGetInStore(float XValue)
    {
        cinemachine.m_XAxis.Value = XValue;
        cinemachine.m_YAxis.m_MaxSpeed = 0;
        cinemachine.m_XAxis.m_Wrap = false;
        cinemachine.m_XAxis.m_MinValue = XValue - 40;
        cinemachine.m_XAxis.m_MaxValue = XValue + 40;
        cinemachine.m_Orbits[0].m_Height = 4;
        cinemachine.m_Orbits[1].m_Height = 4;
        cinemachine.m_Orbits[2].m_Height = 4;

    }
    public void WhenGetOutStore()
    {
        cinemachine.m_YAxis.m_MaxSpeed = 300;
        cinemachine.m_XAxis.m_Wrap = true;
        cinemachine.m_XAxis.m_MinValue = -180;
        cinemachine.m_XAxis.m_MaxValue = 180;
        cinemachine.m_Orbits[0].m_Height = 7;
        cinemachine.m_Orbits[1].m_Height = 4;
        cinemachine.m_Orbits[2].m_Height = -1;
    }
    
    public void StopMovingXYAxis()
        {
            cinemachine.m_XAxis.m_MaxSpeed = 0;
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
        public void MovingXYAxis()
        {
            cinemachine.m_XAxis.m_MaxSpeed = 300;
            cinemachine.m_YAxis.m_MaxSpeed = 2;
        }
    public void StopWalking()
    {
        isCanWalk = false;
        animator.SetBool("isWalk",false);
    }
    public void StartWalking()
    {
        isCanWalk = true;
    }
}

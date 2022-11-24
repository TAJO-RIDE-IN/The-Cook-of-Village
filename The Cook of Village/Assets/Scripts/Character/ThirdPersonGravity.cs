using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonGravity : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 3;
    public float OriginSpeed = 3;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public Animator animator;
    public CinemachineFreeLook cinemachine;

    private GameManager _gameManager;

    private bool isCanWalk = true;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
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
                speed = OriginSpeed * 1.5f;
                animator.SetBool("isRun",true);
            }
            else
            {
                speed = OriginSpeed;
                animator.SetBool("isRun",false);
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
            }
        }
    }
    public void WhenGetInStore()
    {
        cinemachine.m_YAxis.m_MaxSpeed = 0;
    }
    public void WhenGetOutStore()
    {
        cinemachine.m_YAxis.m_MaxSpeed = 300;
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

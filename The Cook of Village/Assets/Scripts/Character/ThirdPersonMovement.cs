using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public Animator charAnimator;
    private Transform _camera;
    public CharacterController controller;
    public float speed = 80f;
    public float originSpeed = 80f;
    public GameObject cameraPosition;

    public bool isLocked;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private SoundManager soundManager;

    private bool isWalkSound;
    private bool isCanWalk = true;
    

    private String walkSound;

    

    private void Awake()
    {
        charAnimator.SetBool("isWalk",false);
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCanWalk)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertival = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertival).normalized;

            if (direction.magnitude >= 0.1f)
            {
                charAnimator.SetBool("isWalk", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.SimpleMove(moveDir.normalized * speed * Time.deltaTime);
                if (!isWalkSound)
                {
                    soundManager.PlayEffect3D(soundManager._audioClips["CookWalk"], gameObject, true);
                    isWalkSound = true;
                }

                if (isLocked)
                {
                    cameraPosition.transform.position = transform.position;
                }
            }
            else
            {
                charAnimator.SetBool("isWalk", false);
                soundManager.StopEffect3D(gameObject);
                isWalkSound = false;
            }
        }
    }
    public void StopWalking()
    {
        isCanWalk = false;
        charAnimator.SetBool("isWalk", false);
    }
    public void StartWalking()
    {
        isCanWalk = true;
    }




}

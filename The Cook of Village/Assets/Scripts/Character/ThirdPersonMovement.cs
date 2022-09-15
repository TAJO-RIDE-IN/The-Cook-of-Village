using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public Animator charAnimator;
    public Transform _camera;
    public CharacterController controller;
    public float speed = 80f;
    public float OriginSpeed = 80f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertival = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertival).normalized;

        if (direction.magnitude >= 0.1f)
        {
            charAnimator.SetBool("isWalk",true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            controller.SimpleMove(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            charAnimator.SetBool("isWalk",false);
        }
        
    }

    
}

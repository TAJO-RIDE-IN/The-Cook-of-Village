using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour, IObserver<GameData>
{
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            StartSleepParticle(obj);
            transform.position = new Vector3(-8, 2.238f, 8);
            cameraMovement.outerUp = 13;
            cameraMovement.outerDown = -11;
            cameraLayer.SecondFloor();
            cameraPosition.transform.position = transform.position;
            cameraLayer.transIn.Change();
            
        }
    }
    public void StartSleepParticle(GameData obj)
    {
        StopWalking();
        particle.gameObject.SetActive(true);
        particle.Play();
        StartCoroutine(ChangeWithDelay.CheckDelay(1.8f, () => WaitParticle(obj)));
    }
    public void WaitParticle(GameData gameData)
    {
        particle.gameObject.SetActive(false);
        gameData.SetTimeMorning();
        StartWalking();
    }
    public void AddObserver(IGameDataOb o)
    {
        o.AddSleepObserver(this);
    }

    public CameraLayer cameraLayer;
    public Animator charAnimator;
    private Transform _camera;
    public CharacterController controller;
    public float speed = 80f;
    public float originSpeed = 80f;
    public GameObject cameraPosition;
    public CameraMovement cameraMovement;
    public ParticleSystem particle;
    public Camera particleCamera;

    public CookingCharacter cookingCharacter;
    public GameObject DishObject;
    public bool isLocked;
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
    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
        set
        {
            isLocked = value;
            if (value)
            {
                cameraPosition.transform.position = transform.position;
                cameraMovement.zoomValue = 0;
            }
            else
            {
                cameraMovement.outerLeft = cameraMovement.outerLeftOriginal;
                cameraMovement.outerRight = cameraMovement.outerRightOriginal;
                cameraMovement.outerDown = cameraMovement.outerDownOriginal;
                cameraMovement.outerUp = cameraMovement.outerUpOriginal;
            }
        }
    }
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
        //particleCamera.depth = 1;
        if (GameData.Instance.isPassOut)
        {
            transform.position = new Vector3(-8, 2.238f, 8);
            cameraPosition.transform.position = new Vector3(-8, 2.238f, 8);
            cameraMovement.outerUp = 13;
            cameraMovement.outerDown = -11;
            cameraLayer.SecondFloor();
            cameraLayer.transIn.Change();
            GameData.Instance.isPassOut = false;

        }
        AddObserver(GameData.Instance);
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
                    soundManager.PlayEffect3D(soundManager._audioClips["CookWalk"], gameObject, true, Pitch);
                    isWalkSound = true;
                }

                if (IsLocked)
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isShift)
            {
                if (cookingCharacter.isHand)
                {
                    DishObject.SetActive(false);
                }
                
                speed = originSpeed * 1.4f;
                charAnimator.SetBool("isRun", true);
                Pitch = 1.8f;
                isShift = true;
                isShiftUp = false;
            }
        }
        else
        {
            if (!isShiftUp)
            {
                if (cookingCharacter.isHand)
                {
                    DishObject.SetActive(true);
                }
                Pitch = 1f;
                speed = originSpeed;
                charAnimator.SetBool("isRun", false);
                isShift = false;
                isShiftUp = true;
            }
        }
    }
    public void StopWalking()
    {
        Debug.Log("∞»¡§∑·");
        isCanWalk = false;
        isWalkSound = false;
        if (soundManager != null)
        {
            soundManager.StopEffect3D(this.gameObject);
        }
        charAnimator.SetBool("isWalk", false);
    }
    public void StartWalking()
    {
        Debug.Log("∞»±‚Ω√¿€");
        isCanWalk = true;
    }




}

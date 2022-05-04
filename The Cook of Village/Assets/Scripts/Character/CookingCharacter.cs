using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CookingCharacter : MonoBehaviour
{
    public Animator frigdeAnimator;
    public Animator charAnimator;
    public GameObject HandPosition;
    
    public MaterialInfos currentMaterial;
    
    private CookingTool _cookingTool;
    private AnimatorOverrideController animatorOverrideController;
    public AnimationClip[] Idle;
    public AnimationClip[] Walk;
    
    private bool isToolCollider;
    public bool isHand;
    
    void Start()
    {
        currentMaterial = null;
        animatorOverrideController = new AnimatorOverrideController(charAnimator.runtimeAnimatorController);
        charAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    private void Update()
    {
        if (isToolCollider)
        {
            MoveInfosToTool();
        }
        
        
    }

    private void FixedUpdate()
    {
        if (!isHand)//null일땐 0 고정, 즉 기본값
        {
            animatorOverrideController["Idle"] = Idle[0];
            animatorOverrideController["Walk"] = Walk[0];
        }
        if(isHand)//어떤 값이라도 들어오면
        {
            animatorOverrideController["Idle"] = Idle[1];
            animatorOverrideController["Walk"] = Walk[1];
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            
            if (Input.GetKey(KeyCode.Space))
            {
                other.transform.GetComponent<Refrigerator>().OpenUI();
                frigdeAnimator.SetBool("isOpen",true);
            }
        }
        
        else if (other.CompareTag("Pot"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>(); 
        }
        else if (other.CompareTag("FryPan"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>(); 
        }
        else if (other.CompareTag("Blender"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>(); 
        }
    }

    private void MoveInfosToTool()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스바눌림");
            if (isHand)//현재 들고있는 재료 정보가 null이 아닐때 넘겨줌, 세부적인건 저기서 수행(UI 바꾸기, 레시피리스트에 ID 추가)
            {
                _cookingTool.currentMaterialInTool = currentMaterial; 
                _cookingTool.PutIngredient();
                for (int i = 0; i < HandPosition.transform.childCount; i++)
                {
                    Destroy(HandPosition.transform.GetChild(i).gameObject);
                }
            }
            isHand = false;
            currentMaterial = null;
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            _cookingTool.Cook();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fridge")
        {
            CloseFridge();
            other.transform.GetComponent<Refrigerator>().CloseUI();
        }
        isToolCollider = false;
    }

    public void CloseFridge()
    {
        frigdeAnimator.SetBool("isOpen",false);
    }
}

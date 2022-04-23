using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CookingCharacter : MonoBehaviour
{
    public GameObject fridgeInven;
    public Animator frigdeAnimator;
    public MaterialInfos currentMaterial;

    public bool isMoved;
    
    public bool isUI = false;
    public bool isToolCollider;

    private CookingTool _cookingTool;

    private void Start()
    {
        currentMaterial = null;
    }

    private void Update()
    {
        if (isToolCollider)
        {
            MoveInfosToTool();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                frigdeAnimator.SetBool("isOpen",true);
                fridgeInven.SetActive(true);
                isUI = true;
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
            if (currentMaterial != null)//현재 들고있는 재료 정보가 null이 아닐때 넘겨줌, 세부적인건 저기서 수행(UI 바꾸기, 레시피리스트에 ID 추가)
            {
                _cookingTool.currentMaterialInTool = currentMaterial; 
                _cookingTool.PutIngredient();
            }
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
            fridgeInven.SetActive(false);
        }
        isToolCollider = false;
    }

    public void CloseFridge()
    {
        frigdeAnimator.SetBool("isOpen",false);
    }
}

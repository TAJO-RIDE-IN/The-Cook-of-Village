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
    
    public bool isUI = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                frigdeAnimator.SetBool("isOpen",true);
                fridgeInven.SetActive(true);
                isUI = true;
                if (!isUI)
                {
                    
                }
                else
                {
                    Debug.Log("꺼짐");
                }
            }
        }
        if (other.CompareTag("Pot"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CookingTool cookingTool = other.transform.GetComponent<CookingTool>(); 
                cookingTool.currentMaterialInTool = currentMaterial; //현재 들고있는 재료 정보를 넘겨줌, 세부적인건 저기서 수행
                if (currentMaterial != null)
                {
                    cookingTool.PutIngredient();
                }
                
                currentMaterial = null;
            }
        }
        if (other.CompareTag("FryPan"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CookingTool cookingTool = other.transform.GetComponent<CookingTool>(); 
                cookingTool.currentMaterialInTool = currentMaterial; //현재 들고있는 재료 정보를 넘겨줌, 세부적인건 저기서 수행
                if (currentMaterial != null)
                {
                    cookingTool.PutIngredient();
                }
                currentMaterial = null;
            }
        }
        if (other.CompareTag("Blender"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CookingTool cookingTool = other.transform.GetComponent<CookingTool>(); 
                cookingTool.currentMaterialInTool = currentMaterial; //현재 들고있는 재료 정보를 넘겨줌, 세부적인건 저기서 수행
                if (currentMaterial != null)
                {
                    cookingTool.PutIngredient();
                }
                currentMaterial = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fridge")
        {
            frigdeAnimator.SetBool("isOpen",false);
            Debug.Log("꺼짐");
            fridgeInven.SetActive(false);
        }
    }
}

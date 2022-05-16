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
    public FoodInfos foodInfos;
    
    private CookingTool _cookingTool;
    private FoodOrder _foodOrder;
    private AnimatorOverrideController animatorOverrideController;
    public AnimationClip[] Idle;
    public AnimationClip[] Walk;
    
    private bool isToolCollider;
    private bool isGuestCollider;
    public bool isHand = false;//이거만 잘 컨트롤해주면 시작할때 null값 넣어주느니 그런거 안해도 되잖아
    private bool isDestroy;
    
    
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
            WhenKeyDown();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingTools"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>(); 
        }
        if (other.CompareTag("Guest"))
        {
            _foodOrder = other.GetComponent<FoodOrder>();
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
    }

    private void WhenKeyDown()//원래 재료넣는 함수였는데 스페이스바누를때 재료만 넣는게 아니고 다양한걸 하는데 스페이스바 누를때 모든 함수 실행하도록 최적화를 위해서 이렇게 하기로함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스바눌림");
            if (isHand)//현재 들고있는 재료 정보가 null이 아닐때 넘겨줌, 세부적인건 저기서 수행(UI 바꾸기, 레시피리스트에 ID 추가)
            {
                if (isToolCollider)//요리도구에 들어갔을때만 재료넣는거 실행
                {
                    if (currentMaterial != null)
                    {
                        PutIngredient();
                        return; //return 잘 썼는지 항상 확인하기
                    }
                }

                if (isGuestCollider)
                {
                    if (foodInfos != null)
                    {
                        Debug.Log("음식 배달완료");
                        _foodOrder.ReceiveFood(foodInfos.ID);
                    }
                }
            }
            else//안들고 있을때 들수 있는거 (냉장고의 재료들은 제외) => 접시, 음식, 밀가루와설탕
            {
                if (isToolCollider)
                {
                    if (_cookingTool.FoodInfos != null)
                    {
                        foodInfos = _cookingTool.FoodInfos;
                        Instantiate(foodInfos.PrefabFood,HandPosition.transform.position, Quaternion.identity, HandPosition.transform);
                    }
                }
                
            }
        }
        
        else if(Input.GetKeyDown(KeyCode.E))
        {
            _cookingTool.Cook();
        }
    }
    public void PutIngredient()
    {
        isDestroy = _cookingTool.PutIngredient(currentMaterial.ID, currentMaterial.ImageUI);
        DestroyOrNot();
        currentMaterial = null;//괜히 손에서 사라진 아이템정보 들고있는거 안좋을까봐
    }

    public void DestroyOrNot()
    {
        if (isDestroy)
        {
            for (int i = 0; i < HandPosition.transform.childCount; i++)//이걸 꽉차면 안없애야하는데..(원래 PutIngredient에 있었음)
            {
                Destroy(HandPosition.transform.GetChild(i).gameObject);
            }
            isHand = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fridge")
        {
            CloseFridge();
            other.transform.GetComponent<Refrigerator>().CloseUI();
            isToolCollider = false;
        }

        if (other.tag == "CookingTools")
        {
            isToolCollider = false;
        }
        
    }

    public void CloseFridge()
    {
        frigdeAnimator.SetBool("isOpen",false);
    }
}

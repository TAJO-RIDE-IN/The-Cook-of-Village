using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CookingCharacter : MonoBehaviour
{
    public Animator charAnimator;
    public GameObject HandPosition;
    
    public IngredientsInfos currentIngredient;
    public FoodInfos currentFood;

    public IngredientsInfos[] ingInv;
    public FoodInfos[] foodInv;
    public int maxInvCount = 4;
    public int curruntInvCount = 0;
    
    
    private CookingTool _cookingTool;
    private FoodOrder _foodOrder;
    private Fridge fridge;
    private AnimatorOverrideController animatorOverrideController;
    private SoundManager soundManager;
    public AnimationClip[] Idle;
    public AnimationClip[] Walk;
    
    private bool isToolCollider;
    private bool isGuestCollider;
    public bool isFridgeCollider;
    private bool isObjectCollider;
    public bool isHand = false;//이거만 잘 컨트롤해주면 시작할때 null값 넣어주느니 그런거 안해도 되잖아
    public bool isFoodInHand;
    public bool isIngInHand;
    private bool isDestroy;
    private string objectName;


    void Start()
    {
        fridge = GameObject.FindGameObjectWithTag("Fridge").GetComponent<Fridge>();
        animatorOverrideController = new AnimatorOverrideController(charAnimator.runtimeAnimatorController);
        charAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    private void Update()
    {

        if (isObjectCollider || isToolCollider || isGuestCollider || isFridgeCollider)
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
    //isHand 참 거짓 상관 없이 실행되어야 하는 것 : 냉장고, 자러가기, 달력 보기

    private void WhenKeyDown()//원래 재료넣는 함수였는데 스페이스바누를때 재료만 넣는게 아니고 다양한걸 하는데 스페이스바 누를때 모든 함수 실행하도록 최적화를 위해서 이렇게 하기로함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isHand)//현재 들고있는 것이 있을 때 실행
            {
                if (isToolCollider)//요리도구에 들어갔을때만,요리중이 아닐때만 재료넣는거 실행
                {
                    if (_cookingTool.isBeforeCooking)
                    {
                        PutIngredient();//세부적인건 툴 스크립트에서 수행(UI 바꾸기, 레시피리스트에 ID 추가)
                        return; //return 잘 썼는지 항상 확인하기
                    }
                    else
                    {
                        //먼저 요리도구를 비우세요! 출력
                    }
                }
                if (isGuestCollider)
                {
                    if (currentFood != null)
                    {
                        isDestroy = _foodOrder.ReceiveFood(currentFood.ID);
                        DestroyOrNot();
                    }
                }
            }
            else//안들고 있을때 들수 있는거 (냉장고의 재료들은 제외) => 접시, 음식, 밀가루와설탕
            {
                if (isToolCollider)
                {
                    if (_cookingTool.isCooked)//요리가 완성됐을때만
                    {
                        currentFood = _cookingTool.FoodInfos;
                        Instantiate(currentFood.PrefabFood,HandPosition.transform.position, Quaternion.identity, HandPosition.transform);
                        isHand = true;
                        isFoodInHand = true;
                        _cookingTool.RefreshTool();
                    }
                    else
                    {
                        
                    }
                }
                
                if (isFridgeCollider)
                {
                    fridge.transform.GetComponent<Fridge>().OpenRefrigerator();
                }

                if (objectName == "Ladder")
                {
                    GameData.Instance.SetTimeMorning();
                    Debug.Log("아침으로 변경");
                }
            }
        }
        
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (isToolCollider)
            {
                if (_cookingTool.isBeforeCooking)//요리 전일때
                {
                    _cookingTool.Cook();
                    //SoundManager.Instance.PlaySE("StartCookOfPan");
                }
                else
                {
                    //먼저 요리도구를 비우세요! 출력
                }
            }
            
            
                
        }
    }
    public void PutIngredient()
    {
        Debug.Log("PutIngredient실행");
        isDestroy = _cookingTool.PutIngredient(currentIngredient.ID, currentIngredient.ImageUI);//값도 넣어주고, bool값도 return하는 함수 실행
        DestroyOrNot();
        
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
            currentIngredient = null;//괜히 손에서 사라진 아이템정보 들고있는거 안좋을까봐
            Debug.Log("재료 없앰");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("CookingTools"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>(); 
            return;
        }

        if (other.CompareTag("Guest"))
        {
            isGuestCollider = true;
            _foodOrder = other.GetComponent<FoodOrder>();
            return;
        }
        if (other.CompareTag("Fridge"))
        {
            isFridgeCollider = true;
            return;
        }
        else
        {
            isObjectCollider = true;
            objectName = other.gameObject.name;
            Debug.Log(other.gameObject.name + "에 진입");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.name == "Flour")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isHand)
                {
                    currentIngredient = IngredientsData.Instance.IngredientsType[0].IngredientsInfos[0];
                    Instantiate(currentIngredient.PrefabMaterial,HandPosition.transform.position, Quaternion.identity, HandPosition.transform);
                    isHand = true;
                    return;
                }
            }
        }
        if (other.gameObject.name == "Sugar")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isHand)
                {
                    currentIngredient = IngredientsData.Instance.IngredientsType[0].IngredientsInfos[1];
                    Instantiate(currentIngredient.PrefabMaterial,HandPosition.transform.position, Quaternion.identity, HandPosition.transform);
                    isHand = true;
                }
            }
        }

        if (other.gameObject.name == "CounterPosition")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("계산스페이스바눌림");
                other.GetComponent<CounterQueue>().PayCounter();
            }
        }

        if (other.gameObject.name == "Trash")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (isHand)
                {
                    for (int i = 0; i < HandPosition.transform.childCount; i++)//이걸 꽉차면 안없애야하는데..(원래 PutIngredient에 있었음)
                    {
                        Destroy(HandPosition.transform.GetChild(i).gameObject);
                    }

                    currentFood = null;
                    currentIngredient = null;
                    isHand = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Fridge")
        {
            other.transform.GetComponent<Fridge>().CloseRefrigerator();
            isFridgeCollider = false;
            return;
        }

        if (other.tag == "CookingTools")
        {
            isToolCollider = false;
            return;
        }
        if (other.CompareTag("Guest"))
        {
            isGuestCollider = false;
        }
        else
        {
            isObjectCollider = false;
        }
    }

}

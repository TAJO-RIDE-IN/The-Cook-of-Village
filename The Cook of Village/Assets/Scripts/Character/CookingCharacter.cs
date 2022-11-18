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
    public GameObject TrashUI;


    [HideInInspector] public CookingTool _cookingTool;
    public Trash trash;
    [HideInInspector] public FoodOrder _foodOrder;
    [HideInInspector] public CookPosition _cookPosition;
    private Fridge fridge;
    private AnimatorOverrideController animatorOverrideController;
    private SoundManager soundManager;
    public AnimationClip[] Idle;
    public AnimationClip[] Walk;
    public UIMovement uiMovement;

    public bool isToolCollider;
    public bool isGuestCollider;
    public bool isFridgeCollider;
    public bool isCookPositionCollider;
    private bool isObjectCollider;
    public bool isHand = false;//이거만 잘 컨트롤해주면 시작할때 null값 넣어주느니 그런거 안해도 되잖아
    public bool isSpace;
    
    private bool isDestroy;

    //[HideInInspector]
    public string objectName;


    void Start()
    {
        isSpace = false;
        fridge = GameObject.FindGameObjectWithTag("Fridge").GetComponent<Fridge>();
        animatorOverrideController = new AnimatorOverrideController(charAnimator.runtimeAnimatorController);
        charAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    private void Update()
    {
        if (isObjectCollider || isToolCollider || isGuestCollider || isFridgeCollider || isCookPositionCollider)
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
            if (isToolCollider)//요리도구에 들어갔을때만,요리중이 아닐때만 재료넣는거 실행
            {
                if (isSpace)
                {
                    isSpace = false;
                    _cookingTool.InventoryBig.SetActive(false);
                    return;
                }
                else
                {
                    isSpace = true;
                    _cookingTool.InventoryBig.SetActive(true);
                    return;
                }
                
            }
            if (isGuestCollider)
            {
                if(isSpace)
                {
                    if (!_foodOrder.CanReceive)
                    {
                        uiMovement.CloseUI();
                        isSpace = false;
                        //UI 띄우기
                        return;
                    }
                    
                }
                else
                {
                    if (_foodOrder.CanReceive)
                    {
                        uiMovement.ShowUI();
                        isSpace = true;
                        //UI 끄기
                        return;
                    }
                    
                }
            }
            if (isFridgeCollider)
            {
                if (isSpace) //냉장고를 닫는 상황
                {
                    fridge.UseRefrigerator(false);
                    isSpace = false;
                    return;
                }
                else // 냉장고를 여는 상황
                {
                    fridge.UseRefrigerator(true);
                    isSpace = true;
                    return;
                }
            }

            if (isCookPositionCollider)
            {
                if (isSpace)
                {
                    _cookPosition.cookPositionUI.gameObject.SetActive(false);
                    isSpace = false;
                    return;
                }
                else
                {
                    _cookPosition.cookPositionUI.gameObject.SetActive(true);
                    isSpace = true;
                    return;
                }
            }

            if (isObjectCollider)
            {
                if (objectName == "Ladder")
                {
                    GameData.Instance.SetTimeMorning();
                    Debug.Log("아침으로 변경");
                    return;
                }

                if (objectName == "Flour")
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[0]
                        .ItemInfos[0]))
                    {
                        return;
                        //쟁반에 밀가루 생성
                    }
                }
                if (objectName == "Sugar")
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[0]
                        .ItemInfos[1]))
                    {
                        return;
                        //쟁반에 밀가루 생성
                    }
                }
                if (objectName == "Cabinet")
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[6]
                        .ItemInfos[3]))
                    {
                        
                        return;
                    }
                    else
                    {
                        ChefInventory.Instance.chefSlotManager.ShowWarning();
                    }
                }
                if (objectName == "Trash")
                {
                    if (!isSpace) //여는 상황
                    {
                        isSpace = true;
                        TrashUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        isSpace = false;
                        TrashUI.SetActive(false);
                    }
                    
                    
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
    
    public IEnumerator FoodOrderUI()
    {
        int TrueNumber = 1;
        int loopNum = 0;
        while (TrueNumber == 1)
        {

            yield return null;

            if(loopNum++ > 10000)
                throw new System.Exception("Infinite Loop");

        }

    }

    public void DestroyOrNot()//쟁반에서 써야할듯
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
        if (other.CompareTag("CookPosition"))
        {
            isCookPositionCollider = true;
            _cookPosition = other.GetComponent<CookPosition>();
            return;
        }
        isObjectCollider = true;
        objectName = other.gameObject.name;
        //Debug.Log(other.gameObject.name + "에 진입");
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "CounterPosition")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                other.GetComponent<CounterQueue>().PayCounter();
            }
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Fridge")
        {
            fridge.UseRefrigerator(false);
            isFridgeCollider = false;
            isSpace = false;
            return;
        }

        if (other.tag == "CookingTools")
        {
            isToolCollider = false;
            isSpace = false;
            _cookingTool.InventoryBig.SetActive(false);
            return;
        }
        if (other.CompareTag("Guest"))
        {
            uiMovement.CloseUI();
            isSpace = false;
            isGuestCollider = false;
            
        }

        if (other.CompareTag("CookPosition"))
        {
            _cookPosition.cookPositionUI.gameObject.SetActive(false);
            isCookPositionCollider = false;
        }
        if (other.gameObject.name == "Trash")
        {
            TrashUI.SetActive(false);
            isSpace = false;
            isObjectCollider = false;
        }
        
        else
        {
            isObjectCollider = false;
            isSpace = false;
            
        }
    }

}

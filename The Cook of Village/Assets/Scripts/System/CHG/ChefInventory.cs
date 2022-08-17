using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChefInventory : MonoBehaviour
{
    
    private static ChefInventory instance = null;
    
    private void Awake()
    {
        for (int i = 0; i < MaxInven; i++)
        {
            EdibleItems[i] = new EdibleItem()
                {_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = null, _foodInfos = null};
        }
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static ChefInventory Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public CookItemSlotManager cookSlotManager;
    public ChefItemSlotManager chefSlotManager;
    private int maxInven = 2;//이 값이 바뀌면 인벤토리 잠금을 해제할거니깐 초기화도 게임데이터에서 하면 좋을듯
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int MaxInven
    {
        get { return maxInven;}
        set
        {
            maxInven = value;
            ExtensionInventory();
        }
    }
    private int wholeInven = 6;//이건 나중에 하자..
    public int WholeInven
    {
        get { return maxInven;}
        set
        {
            maxInven = value;
            ExtensionInventory();
        }
    }
    
    [Serializable]
    public class EdibleItem
    {
        [Serializable]
        public enum ItemType
        {
            Ingredient, Food
        }
        [SerializeField] public ItemType _itemType;
        [SerializeField] public ItemInfos _ingredientsInfos;
        [SerializeField] public FoodInfos _foodInfos;
    }


    [SerializeField] private EdibleItem[] EdibleItems = new EdibleItem[6];

    public bool[] isUsed = Enumerable.Repeat(false, 6).ToArray();
        
        
        //= new List<EdibleItem>()

    private void Start()
    {
        for (int i = 0; i < MaxInven; i++)
        {
            EdibleItems[i]._itemType = EdibleItem.ItemType.Ingredient;
        }
        _cookingCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<CookingCharacter>();

    }
    private void ExtensionInventory()
    {
        //UI 바꿈
        //버튼 Interactable 켜주기
    }

    public bool AddIngredient(ItemInfos ingredient)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            //Debug.Log(i+"번째 슬롯 진입");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"번째 슬롯이 비어있음");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Ingredient;
                EdibleItems[i]._ingredientsInfos = ingredient;
                EdibleItems[i]._foodInfos = null;
                chefSlotManager.AddIngredientItem(ingredient, i);
                isUsed[i] = true;
                return true;
            }
        }
        chefSlotManager.ShowWarning();
        return false;
        
    }
    public bool AddFood(FoodInfos food)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"번째 슬롯이 비어있음");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                return true;
            }
            //SlotManager.AddFoodItem(food);
        }
        chefSlotManager.ShowWarning();
        return false;
    }


    public void SendItem(int i)
    {
        if (isUsed[i])
        {
            if (_cookingCharacter.isSpace)
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //스페이스바를 냉장고안이나에서 누른건 아닌지 확인 => 스위치로 변경
                    {
                        if (_cookingCharacter._cookingTool.isBeforeCooking)//요리하기 전이 맞는지 확인
                        {
                            if (_cookingCharacter._cookingTool.PutIngredient(EdibleItems[i]._ingredientsInfos.ID,
                                EdibleItems[i]._ingredientsInfos.ImageUI))
                            {
                                EdibleItems[i]._ingredientsInfos = null;
                                isUsed[i] = false;
                                chefSlotManager.itemslots[i].changeSlotUI(chefSlotManager.emptySlot);
                                return;
                            };
                        
                        }
                    }
                }

                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Food)
                {
                    if (_cookingCharacter.isGuestCollider)
                    {
                        Debug.Log("음식 전달 완료");
                        //게스트가 안받은 상태라면
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            isUsed[i] = false;
                            chefSlotManager.itemslots[i].changeSlotUI(chefSlotManager.emptySlot);
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = EdibleItems[i]._foodInfos.ImageUI;
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //요리 전달
            }
        }
        
        
        
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    private static InventoryManager instance = null;
        
    private void Awake()
    {
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
    public static InventoryManager Instance
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
    public EdibleItemSlotManager edibleSlotManager;
    private int maxInven = 2;//이 값이 바뀌면 인벤토리 잠금을 해제할거니깐 초기화도 게임데이터에서 하면 좋을듯
    private CookingCharacter _cookingCharacter;

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
        [SerializeField] public IngredientsInfos _ingredientsInfos;
        [SerializeField] public FoodInfos _foodInfos;
    }


    [SerializeField] private EdibleItem[] EdibleItems = new EdibleItem[6];

    /*public EdibleItem[] EdibleItems
    {
        get { return edibleItems;}
        set
        {
            edibleItems = value;
            
        }
    }*/
    public bool[] isUsed = Enumerable.Repeat(false, 6).ToArray();
        
        
        //= new List<EdibleItem>()

    private void Start()
    {
        _cookingCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<CookingCharacter>();

    }
    private void ExtensionInventory()
    {
        //UI 바꿈
        //버튼 Interactable 켜주기
    }

    public bool AddIngredient(IngredientsInfos infos)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            Debug.Log(i+"번째 슬롯 진입");
            if (isUsed[i] == false)
            {
                Debug.Log(i+"번째 슬롯이 비어있음");
                EdibleItems[i] = new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = infos, 
                    _foodInfos = null};
                edibleSlotManager.AddIngredientItem(infos, i);
                isUsed[i] = true;
                return true;
            }
        }
        edibleSlotManager.ShowWarning();
        return false;
        
    }
    public bool AddFood(FoodInfos food)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            if (isUsed[i] == false)
            {
                Debug.Log(i+"번째 슬롯이 비어있음");
                EdibleItems[i] = new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = null, 
                    _foodInfos = food};
                edibleSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                return true;
            }
            //SlotManager.AddFoodItem(food);
        }
        edibleSlotManager.ShowWarning();
        return false;
    }

    public void UseIngredient(int i)
    {
        
    }

    public void SendItem(int i)
    {
        if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
        {
            if (_cookingCharacter.isInvenSpace) //우선 캐릭터가 스페이스바를 눌렀는지 확인
            {
                if (_cookingCharacter.isToolCollider) //스페이스바를 냉장고안이나에서 누른건 아닌지 확인 => 스위치로 변경
                {
                    if (_cookingCharacter._cookingTool.isBeforeCooking)//요리하기 전이 맞는지 확인
                    {
                        if (_cookingCharacter._cookingTool.PutIngredient(EdibleItems[i]._ingredientsInfos.ID,
                            EdibleItems[i]._ingredientsInfos.ImageUI))
                        {
                            EdibleItems[i] = null;
                            isUsed[i] = false;
                            edibleSlotManager.itemslots[i].changeSlotUI(edibleSlotManager.emptySlot);
                        };
                        
                    }
                }
                //_cookingCharacter._cookingTool
            }
        }
        else
        {
            //요리 전달
        }
        
    }
    
}

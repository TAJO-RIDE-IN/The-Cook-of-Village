using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlotManager SlotManager;
    public GameObject SlotParent;
    private int maxInven = 2;//이 값이 바뀌면 인벤토리 잠금을 해제할거니깐 초기화도 게임데이터에서 하면 좋을듯
    private void Start()
    {
        
    }
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
    private bool[] isUsed = Enumerable.Repeat(false, 6).ToArray();
        
        
        //= new List<EdibleItem>()

    private void ExtensionInventory()
    {
        //UI 바꿈
    }

    public void AddOrNot()
    {
        if (EdibleItems.Length < maxInven)
        {
            //AddIngredient();
        }
        else
        {
            SlotManager.ShowWarning();
        }
    }

    public void AddIngredient(IngredientsInfos infos)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            if (isUsed[i] == false)
            {
                EdibleItems[i] = new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = infos, 
                    _foodInfos = null};
                SlotManager.AddIngredientItem(infos);
                isUsed[i] = true;
                return;
            }
            

            
        }
        SlotManager.ShowWarning();
    }
    public void AddFood(FoodInfos food)
    {
        for (int i = 0; i < MaxInven; i++)
        {
            EdibleItems[i] = new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = null, 
                _foodInfos = food};
            SlotManager.AddFoodItem(food);

            return;
        }
        SlotManager.ShowWarning();
    }

    public void UseIngredient(int i)
    {
        
    }
}

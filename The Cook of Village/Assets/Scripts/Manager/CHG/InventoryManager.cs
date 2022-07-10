using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlotManager SlotManager;
    private int maxInven = 2;//이 값이 바뀌면 인벤토리 잠금을 해제할거니깐 초기화도 게임데이터에서 하면 좋을듯
    
    public int MaxInven
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
    

    [SerializeField] private List<EdibleItem> edibleItems = new List<EdibleItem>();

    private void ExtensionInventory()
    {
        //UI 바꿈
    }

    public void AddOrNot()
    {
        if (edibleItems.Count < maxInven)
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
        if (edibleItems.Count < maxInven)
        {
            Debug.Log("리스트 추가");
            edibleItems.Add(new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = infos, 
                _foodInfos = null});
        }
        else
        {
            SlotManager.ShowWarning();
        }
    }
    public void AddFood(FoodInfos food)
    {
        if (edibleItems.Count < maxInven)
        {
            edibleItems.Add(new EdibleItem(){_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = null, 
                _foodInfos = food});
        }
        else
        {
            SlotManager.ShowWarning();
        }
    }
}

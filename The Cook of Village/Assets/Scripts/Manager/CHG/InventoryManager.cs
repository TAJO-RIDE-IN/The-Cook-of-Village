using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int maxInven;
    
    public int MaxInven
    {
        get { return maxInven;}
        set
        {
            maxInven = value;
            //EdibleItems.Add();
        }
    }
    
    [Serializable]
    public class EdibleItem
    {
        [Serializable]
        enum ItemType
        {
            Ingredient, Food
        }
        [SerializeField] private ItemType _itemType;
        [SerializeField] private IngredientsData _ingredientsData;
        [SerializeField] private FoodData _foodData;
    }

    [SerializeField] public List<EdibleItem> EdibleItems = new List<EdibleItem>();

    private void ExtensionInventory()
    {
        
    }
}

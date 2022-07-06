using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleData : ItemData
{
    public enum ItemType
        {
            Ingredient, Food
        }
    
    public ItemType itemType => _itemType;
    [SerializeField] private ItemType _itemType;
}

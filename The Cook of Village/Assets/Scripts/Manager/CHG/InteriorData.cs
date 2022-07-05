using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorData : ItemData
{
    public enum ItemType
    {
        Installation, Switch
    }
    
    public ItemType itemType => _itemType;
    [SerializeField] private ItemType _itemType;
}

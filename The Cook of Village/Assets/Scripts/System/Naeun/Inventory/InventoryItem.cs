using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    public int ID => _id;
    public enum ItemType { Food, Ingredient, Potion, CookingTool, Furniture }
    public string Name => _name;
    public int Price => _price;
    public string Explanation => _explanation;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] private int _id;
    [SerializeField] private ItemType _itemtype;
    [SerializeField] private string _name;    // 아이템 이름
    [SerializeField] private int _price;    // 아이템 이름
    [Multiline]
    [SerializeField] private string _explanation; // 아이템 설명
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private GameObject ItemPrefab;

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
    public abstract Item CreateItem();
}

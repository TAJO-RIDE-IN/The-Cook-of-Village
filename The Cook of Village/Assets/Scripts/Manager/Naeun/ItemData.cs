using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ItemInfos
{
    public ItemType.Type type;
    public int ID;
    public string Name;
    [Multiline]
    public string Explanation;
    public int Price;
    public int Amount;
    public GameObject ItemPrefab;
    public Sprite ImageUI;
}

[System.Serializable]
public class ItemType
{
    public enum Type { Base, Fruit, Vegetable, Meat, Other, Potion, CookingTool, Furniture }
    [SerializeField]
    public Type type;
    public List<ItemInfos> ItemInfos = new List<ItemInfos>();
}

public class ItemData : DataManager
{
    #region Singleton, LoadData
    private static ItemData instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<ItemType>(ref ItemType, "ItemData"); //data 완성 되었을때 다시 활성화
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ItemData Instance
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
    #endregion
    public int MaxMaterialCount = 99;
    [SerializeField]
    public ItemType[] ItemType;

    public override void SaveDataTime()
    {
        SaveArrayData<ItemType>(ref ItemType, "ItemData");
    }

    public ItemInfos ItemInfos(int id)
    {
        int dataIndex;
        dataIndex = ItemType[IngredientType(id)].ItemInfos.FindIndex(m => m.ID == id);
        return ItemType[IngredientType(id)].ItemInfos[dataIndex];
    }
    private int IngredientType(int id)
    {
        return id / 10;
    }
    public void ChangeAmount(int id, int amount)
    {
        ItemInfos(id).Amount = amount;
    }
}

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
    public string KoreanName;
    [Multiline]
    public string Explanation;
    public int Price;
    public int Amount;
    public int ShopCount;
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
    public List<ItemInfos> IngredientList()
    {
        List<ItemInfos> _itemInfos = new List<ItemInfos>();
        for(int i = 1; i < 5; i++)
        {
            _itemInfos.AddRange(ItemType[i].ItemInfos);
        }
        return _itemInfos;
    }
    private int IngredientType(int id)
    {
        return id / 10;
    }

    /// <summary>
    /// TooId로 ItemInfos 찾기
    /// </summary>
    /// <param name="toolID">FoodTool type의 int값 입력</param>
    /// <returns>TooId와 같은 아이템의 ItemInfos 리턴</returns>
    public ItemInfos ToolIdToItem(int toolID)
    {
        int id = toolID + 60;
        ItemInfos infos = ItemInfos(id);
        return infos;
    }


    /// <summary>
    /// Item의 개수 변경, CookingTool인 경우 사용 가능하도록 변경
    /// </summary>
    /// <param name="id">ItemInfos의 ID 입력</param>
    /// <param name="amount">변경하고 싶은 개수 입력</param>
    public void ChangeAmount(int id, int amount)
    {
        ItemInfos infos = ItemInfos(id);
        infos.Amount = amount;
        if(amount > 0 && infos.type == global::ItemType.Type.CookingTool)
        {
            FoodData.Instance.CanUseTool(infos.ID);
        }
    }
}

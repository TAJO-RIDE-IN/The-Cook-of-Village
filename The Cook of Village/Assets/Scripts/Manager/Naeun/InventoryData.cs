using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemInfos
{
    public int Type;
    public int ID;
    public string Name;
    public string Explanation;
    public int Price;
    public int Amount;
}
[System.Serializable]
public class InventoryType
{
    public enum Type {Potion, CookingTool, Ingredient}
    [SerializeField]
    public Type type;
    public List<InventoryItemInfos> InventoryInfos = new List<InventoryItemInfos>();
}

public class InventoryData : DataManager
{
    #region Singleton, LoadData
    private static InventoryData instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<MaterialType>(ref IngredientsType, "IngredientsData"); //data 완성 되었을때 다시 활성화
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static InventoryData Instance
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
    public InventoryType[] InventoryType;
    public override void SaveDataTime()
    {
        SaveArrayData<InventoryType>(ref InventoryType, "InventoryData");
    }
    public InventoryItemInfos InventoryItemInfos(int id, int type)
    {
        int dataIndex;
        dataIndex = InventoryType[type].InventoryInfos.FindIndex(m => m.ID == id);
        return InventoryType[type].InventoryInfos[dataIndex];
    }
}

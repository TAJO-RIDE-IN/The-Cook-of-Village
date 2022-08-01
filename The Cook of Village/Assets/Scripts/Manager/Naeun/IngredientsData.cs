using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class IngredientsInfos
{
    public int Type;
    public int ID;
    public string Name;
    public int Price;
    public int Amount;
    public GameObject PrefabMaterial;
    public Sprite ImageUI; //UIImage
}

[System.Serializable]
public class IngredientsType
{
    public enum Type { Base, Fruit, Vegetable, Meat }
    [SerializeField]
    public Type type;
    public List<IngredientsInfos> IngredientsInfos = new List<IngredientsInfos>();
}

public class IngredientsData : DataManager
{
    #region Singleton, LoadData
    private static IngredientsData instance = null;
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
    public static IngredientsData Instance
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
    public IngredientsType[] IngredientsType;

    public override void SaveDataTime()
    {
        SaveArrayData<IngredientsType>(ref IngredientsType, "IngredientsData");
    }

    public IngredientsInfos IngredientsInfos(int id)
    {
        int dataIndex;
        dataIndex = IngredientsType[IngredientType(id)].IngredientsInfos.FindIndex(m => m.ID == id);
        return IngredientsType[IngredientType(id)].IngredientsInfos[dataIndex];
    }
    private int IngredientType(int id)
    {
        return id / 10;
    }
    public void ChangeAmount(int type, int id, int amount)
    {
        IngredientsInfos(id).Amount = amount;
    }
}

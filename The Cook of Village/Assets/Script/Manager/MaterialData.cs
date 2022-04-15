/////////////////////////////////////
/// ÌïôÎ≤à : 91914200
/// Ïù¥Î¶Ñ : JungNaEun Ï†ïÎÇòÏùÄ
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class MaterialInfos
{
    public int Type;
    public int ID;
    public string Name;
    public int Price;
    public int Amount; //playerÍ∞Ä ÏÜåÏßÄÏ§ëÏù∏ Í∞úÏàò
    public GameObject PrefabMaterial;
    public Sprite ImageUI; //UIImage

    public MaterialInfos(int type,int id, string name, int price, int amount, GameObject prefab, Sprite imageUI)
    {
        Type = type;
        ID = id;
        Name = name;
        Price = price;
        Amount = amount;
        PrefabMaterial = prefab;
        ImageUI = imageUI;
    }
}

[System.Serializable]
public class Material
{
    public enum Type { Base, Fruit, Vegetable, Meat }
    [SerializeField]
    public Type type;
    public List<MaterialInfos> materialInfos = new List<MaterialInfos>();
    public Material(Type _type, List<MaterialInfos> _materialInfos)
    {
        type = _type;
        materialInfos = _materialInfos;
    }
}

public class MaterialData : DataManager
{
    #region Singleton, LoadData
    private static MaterialData instance = null;
    private void Awake() //æ¿ Ω√¿€µ…∂ß ¿ŒΩ∫≈œΩ∫ √ ±‚»≠
    {
        if (null == instance)
        {
            instance = this;
            LoadData<Material>(ref material, "MaterialData");
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MaterialData Instance
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
    public Material[] material;
    [ContextMenu("To Json Data")]
    public override void SaveDataTime()
    {
        SaveData<Material>(ref material, "MaterialData");
    }

    public void ChangeAmount(int type, int id, int amount)
    {
        int dataIndex = material[type].materialInfos.FindIndex(m => m.ID == id);
        material[type].materialInfos[dataIndex].Amount = amount;
    }

}

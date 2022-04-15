/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
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
    public int Amount; //player�� �������� ����
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
public class MaterialType
{
    public enum Type { Base, Fruit, Vegetable, Meat }
    [SerializeField]
    public Type type;
    public List<MaterialInfos> materialInfos = new List<MaterialInfos>();
    public MaterialType(Type _type, List<MaterialInfos> _materialInfos)
    {
        type = _type;
        materialInfos = _materialInfos;
    }
}

public class MaterialData : DataManager
{
    #region Singleton, LoadData
    private static MaterialData instance = null;
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<Material>(ref material, "MaterialData"); //data �ϼ� �Ǿ����� �ٽ� Ȱ��ȭ
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
    public MaterialType[] materialType;
    [ContextMenu("To Json Data")]
    public override void SaveDataTime()
    {
        SaveData<MaterialType>(ref materialType, "MaterialData");
    }

    public void ChangeAmount(int type, int id, int amount)
    {
        int dataIndex = materialType[type].materialInfos.FindIndex(m => m.ID == id);
        materialType[type].materialInfos[dataIndex].Amount = amount;
    }

}

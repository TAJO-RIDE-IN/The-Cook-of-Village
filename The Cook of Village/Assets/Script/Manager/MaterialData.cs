/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class MaterialInfos
{
    public int ID;
    public string Name;
    public int Price;
    public int Amount; //player가 소지중인 개수
    public GameObject PrefabMaterial;
    public Image ImageUI; //UIImage

    public MaterialInfos(int id, string name, int price, int amount, GameObject prefab, Image imageUI)
    {
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
    public enum Type { Fruit, Vegetable, Meat }
    [SerializeField]
    public Type type;
    public List<MaterialInfos> materialInfos = new List<MaterialInfos>();
    public Material(Type _type, List<MaterialInfos> _materialInfos)
    {
        type = _type;
        materialInfos = _materialInfos;
    }
}

public class MaterialData : MonoBehaviour
{
    [SerializeField]
    public Material[] material;
    [ContextMenu("To Json Data")]
    public void SaveNoteData()
    {
        string toJson = JsonHelper.arrayToJson(material, prettyPrint: true);
        File.WriteAllText(Application.dataPath + "/Resources/Data/MaterialData.json", toJson);
    }
    public void LoadNoteData()
    {
        string path = "Data/MaterialData";
        TextAsset jsonData = Resources.Load(path) as TextAsset;
        material = JsonHelper.getJsonArray<Material>(jsonData.ToString());
    }

    public void ChangeAmount(int type, int order, int amount)
    {
        material[type].materialInfos[order].Amount = amount;
    }
}

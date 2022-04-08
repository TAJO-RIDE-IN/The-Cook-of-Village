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
    public int Possession; //player가 소지중인 개수
    public Sprite Image3D; //3d Image
    public Image ImageUI; //UIImage

    public MaterialInfos(int type, int id, string name, int price, int possession, Sprite image3D, Image imageUI)
    {
        ID = id;
        Name = name;
        Price = price;
        Possession = possession;
        Image3D = image3D;
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
        //string path = "/Data/MaterialData.json";
        string toJson = JsonHelper.arrayToJson(material, prettyPrint: true);
        //File.WriteAllText(Application.streamingAssetsPath + path, toJson);
        File.WriteAllText(Application.dataPath + "/Resources/Data/MaterialData.json", toJson);
    }
    public void LoadNoteData()
    {
/*        string path = "/Data/MaterialData.json";
        string jsonData = File.ReadAllText(Application.streamingAssetsPath + path);
        material = JsonHelper.getJsonArray<Material>(jsonData);*/

        string path = "Data/MaterialData";
        TextAsset jsonData = Resources.Load(path) as TextAsset;
        material = JsonHelper.getJsonArray<Material>(jsonData.ToString());
    }

}

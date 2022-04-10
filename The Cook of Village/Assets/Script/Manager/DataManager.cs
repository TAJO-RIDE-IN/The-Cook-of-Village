using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    public Material[] material;
    [ContextMenu("To Json Data")]
    protected void SaveNoteData()
    {
        string toJson = JsonHelper.arrayToJson(material, prettyPrint: true);
        File.WriteAllText(Application.dataPath + "/Resources/Data/MaterialData.json", toJson);
    }
    protected void LoadNoteData(string data)
    {
        string path = "Data/" + data;
        TextAsset jsonData = Resources.Load(path) as TextAsset;
        material = JsonHelper.getJsonArray<Material>(jsonData.ToString());
    }

}

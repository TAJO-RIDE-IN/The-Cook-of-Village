using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataManager : MonoBehaviour
{
    public abstract void SaveDataTime(); //하루가 지날 때마다 저장

    [ContextMenu("To Json Data")]
    protected void SaveData<T>(ref T[] source, string FileName)
    {
        string toJson = JsonHelper.arrayToJson(source, prettyPrint: true);
        File.WriteAllText(Application.dataPath + "/Resources/Data/" + FileName + ".json", toJson);
    }
    protected void LoadData<T>(ref T[] source, string FileName)
    {
        string DataPath = "Data/" + FileName;
        TextAsset jsonData = Resources.Load(DataPath) as TextAsset;
        source = JsonHelper.getJsonArray<T>(jsonData.ToString());
    }
}

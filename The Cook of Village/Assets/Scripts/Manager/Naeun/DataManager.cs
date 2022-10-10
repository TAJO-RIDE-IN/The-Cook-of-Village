using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataManager : MonoBehaviour
{
    public abstract void SaveDataTime(); //하루가 지날 때마다 저장

    [ContextMenu("To Json Data")]
    protected void SaveData<T>(ref T source, string FileName)
    {
        string toJson = JsonUtility.ToJson(source, prettyPrint: true);
        File.WriteAllText(Application.persistentDataPath + "/" + FileName + ".json", toJson);
    }
    protected void SaveArrayData<T>(ref T[] source, string FileName)
    {
        string toJson = JsonHelper.arrayToJson(source, prettyPrint: true);
        File.WriteAllText(Application.persistentDataPath + "/"+ FileName + ".json", toJson);
    }
    protected void LoadData<T>(ref T source, string FileName)
    {
        string DataPath = Application.persistentDataPath + "/" + FileName + ".json";
        string json = File.ReadAllText(DataPath);
        source = JsonUtility.FromJson<T>(json);
    }
    protected void LoadArrayData<T>(ref T[] source, string FileName)
    {
        string DataPath = Application.persistentDataPath + "/" + FileName + ".json";
        string json = File.ReadAllText(DataPath);
        source = JsonHelper.getJsonArray<T>(json);
    }
    protected bool FileExists(string FileName)
    {
        string DataPath = Application.persistentDataPath + "/" + FileName + ".json";
        FileInfo File = new FileInfo(DataPath);
        return File.Exists;
    }
}

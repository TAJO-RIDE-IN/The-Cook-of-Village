using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataManager<T> : Singletion<T> where T : MonoBehaviour
{
    public abstract void SaveDataTime(int PlayNum); //6시간이 지날때 마다 저장

    [ContextMenu("To Json Data")]
    protected void SaveData<D>(ref D source, string FileName)
    {
        string toJson = JsonUtility.ToJson(source, prettyPrint: true);
        File.WriteAllText(DataPath(FileName), toJson);
    }
    protected void SaveArrayData<D>(ref D[] source, string FileName)
    {
        string toJson = JsonHelper.arrayToJson(source, prettyPrint: true);
        File.WriteAllText(DataPath(FileName), toJson);
    }
    protected void LoadData<D>(ref D source, string FileName)
    {
        string json = File.ReadAllText(DataPath(FileName));
        source = JsonUtility.FromJson<D>(json);
    }
    protected void LoadArrayData<D>(ref D[] source, string FileName)
    {
        string json = File.ReadAllText(DataPath(FileName));
        source = JsonHelper.getJsonArray<D>(json);
    }
    private string DataPath(string FileName)
    {
        string path = Application.persistentDataPath + "/" + FileName + ".json";
        return path;
    }
    protected bool FileExists(string FileName)
    {
        string DataPath = Application.persistentDataPath + "/" + FileName + ".json";
        FileInfo File = new FileInfo(DataPath);
        return File.Exists;
    }
}

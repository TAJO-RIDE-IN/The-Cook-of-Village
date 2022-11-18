using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataManager<T> : Singletion<T> where T : MonoBehaviour
{
    public abstract void SaveDataTime(string PlayNum); //6시간이 지날때 마다 저장

    [ContextMenu("To Json Data")]
    protected void SaveData<D>(ref D source, string FileName, string PlayName)
    {
        string toJson = JsonUtility.ToJson(source, prettyPrint: true);
        File.WriteAllText(DataPath(FileName, PlayName), toJson);
    }
    protected void SaveArrayData<D>(ref D[] source, string FileName, string PlayName)
    {
        string toJson = JsonHelper.arrayToJson(source, prettyPrint: true);
        File.WriteAllText(DataPath(FileName, PlayName), toJson);
    }
    protected void LoadData<D>(ref D source, string FileName, string PlayName)
    {
        string json = File.ReadAllText(DataPath(FileName, PlayName));
        source = JsonUtility.FromJson<D>(json);
    }
    protected void LoadArrayData<D>(ref D[] source, string FileName, string PlayName)
    {
        string json = File.ReadAllText(DataPath(FileName, PlayName));
        source = JsonHelper.getJsonArray<D>(json);
    }
    private string DataPath(string FileName, string PlayName)
    {
        string folder_path = Application.persistentDataPath + "/" + PlayName;
        if(!Directory.Exists(folder_path))
        {
            Directory.CreateDirectory(folder_path);
        }
        string path = folder_path + "/" + FileName + ".json";
        return path;
    }
    protected bool FileExists(string FileName)
    {
        string DataPath = Application.persistentDataPath + "/" + FileName + ".json";
        FileInfo File = new FileInfo(DataPath);
        return File.Exists;
    }
}

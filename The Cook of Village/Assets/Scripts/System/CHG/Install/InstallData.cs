using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class IndexName
{
    public int index;
    public string name;

    public IndexName(int a, string b)
    {
        index = a;
        name = b;
    }
}

[System.Serializable]
public class ToolData
{
    public List<IndexName> _indexNames = new List<IndexName>();
}

public class InstallData
{
    public static ToolData toolData = new ToolData();
    
    private static string jsonData;
    private static string path = Application.persistentDataPath + "ToolData.json";
    
    void Awake()
    {
        Debug.Log(path);
        jsonData = File.ReadAllText(path);
        toolData = JsonUtility.FromJson<ToolData>(jsonData);
    }
    
    public static void SaveData(int index, string name)
    {
        toolData._indexNames.Add(new IndexName(index, name));
        jsonData = JsonUtility.ToJson(toolData, true);
        File.WriteAllText(path, jsonData);
    }
    
}

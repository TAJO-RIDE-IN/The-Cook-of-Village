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

public class InstallData : MonoBehaviour
{
    public static ToolData toolData = new ToolData();
    
    
    private static string path = Application.persistentDataPath + "ToolData.json";
    private static string jsonData = File.ReadAllText(path);

    static InstallData()
    {
        toolData = JsonUtility.FromJson<ToolData>(jsonData);
    }
    
    public static void SaveData(int index, string name)
    {
        toolData._indexNames.Add(new IndexName(index, name));
        jsonData = JsonUtility.ToJson(toolData, true);
        File.WriteAllText(path, jsonData);
    }

    //public void RestoreHealth(int amount) => health += amount;
    public static void DeleteData(int deleteIndex)
    {
        for (int i = 0; i < toolData._indexNames.Count; i++)
        {
            if (toolData._indexNames[i].index == deleteIndex)
            {
                toolData._indexNames.RemoveAt(i);
                jsonData = JsonUtility.ToJson(toolData, true);
                File.WriteAllText(path, jsonData);
            }
        }
    }
    
}

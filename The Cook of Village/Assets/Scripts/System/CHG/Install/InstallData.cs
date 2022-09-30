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
    public ToolData toolData;
    
    private string jsonData;
    private string path;
    void Awake()
    {
        path = Application.dataPath + "/Resources" + "/Data" + "/ToolData.json";
        jsonData = File.ReadAllText(path);
        toolData = JsonUtility.FromJson<ToolData>(jsonData);
        
    }
    
    public void SaveData(int index, string name)
    {
        toolData._indexNames.Add(new IndexName(index, name));
        Debug.Log(toolData._indexNames[0].index);
        jsonData = JsonUtility.ToJson(toolData, true);
        File.WriteAllText(path, jsonData);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

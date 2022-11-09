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

[System.Serializable]
public class FurnitureData
{
    public List<IndexName> _indexNames = new List<IndexName>();
}

public class InstallData
{
    public enum SortOfInstall
    {
        Tool,
        Furnitue
    };
    
    public static ToolData toolData = new ToolData();
    public static FurnitureData furnitureData = new FurnitureData();

    private static string toolPath = Application.persistentDataPath + "ToolData.json";
    private static string toolJsonData;
    
    private static string furniturePath = Application.persistentDataPath + "FurnitureData.json";
    private static string furnitureJsonData;

    static InstallData()
    {
        FileInfo fileInfo = new FileInfo(toolPath);
        if (fileInfo.Exists)
        {
            toolJsonData = File.ReadAllText(toolPath);
        }
        else
        {
            FirstSavePath();
        }
        
        FileInfo fileInfo2 = new FileInfo(furniturePath);
        if (fileInfo2.Exists)
        {
            furnitureJsonData = File.ReadAllText(furniturePath);
        }
        else
        {
            FirstSavePath2();
        }

        toolData = JsonUtility.FromJson<ToolData>(toolJsonData);
        furnitureData = JsonUtility.FromJson<FurnitureData>(furnitureJsonData);
    }

    private static void FirstSavePath()
    {
        toolJsonData = JsonUtility.ToJson(toolData, true);
        File.WriteAllText(toolPath, toolJsonData);
    }
    private static void FirstSavePath2()
    {
        furnitureJsonData = JsonUtility.ToJson(furnitureData, true);
        File.WriteAllText(furniturePath, furnitureJsonData);
    }
    
    
    
    public static void SaveData(int index, string name, SortOfInstall sortOfInstall)
    {
        switch (sortOfInstall)
        {
            case SortOfInstall.Furnitue:
            {
                furnitureData._indexNames.Add(new IndexName(index, name));
                furnitureJsonData = JsonUtility.ToJson(furnitureData, true);
                File.WriteAllText(furniturePath, furnitureJsonData);
                break;
            }
            case SortOfInstall.Tool:
            {
                toolData._indexNames.Add(new IndexName(index, name));
                toolJsonData = JsonUtility.ToJson(toolData, true);
                File.WriteAllText(toolPath, toolJsonData);
                break;
            }
        }
        
    }

    //public void RestoreHealth(int amount) => health += amount;
    public static void DeleteData(int deleteIndex, SortOfInstall sortOfInstall)
    {
        for (int i = 0; i < toolData._indexNames.Count; i++)
        {
            if (toolData._indexNames[i].index == deleteIndex)
            {
                toolData._indexNames.RemoveAt(i);
                toolJsonData = JsonUtility.ToJson(toolData, true);
                File.WriteAllText(toolPath, toolJsonData);
            }
        }
    }
    
}

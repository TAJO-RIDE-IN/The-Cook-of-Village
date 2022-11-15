using System;
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

public class InstallData : DataManager
{

    public override void SaveDataTime()
    {
        //SaveArrayData<NPCInfos>(ref npcInfos, "NPCData");
    }
    
    private static InstallData instance;
    public static InstallData Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    
    public enum SortOfInstall
    {
        Tool,
        Furnitue
    };
    
    public static ToolData toolData = new ToolData();
    public static FurnitureData furnitureData = new FurnitureData();

    private static string toolPath;
    private static string toolJsonData;
    
    private static string furniturePath;
    private static string furnitureJsonData;

    

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        toolPath = Application.persistentDataPath + "ToolData.json";
        furniturePath = Application.persistentDataPath + "FurnitureData.json";
        
        FileInfo fileInfo = new FileInfo(toolPath);
        if (fileInfo.Exists)
        {
            toolJsonData = File.ReadAllText(toolPath);
        }
        else
        {
            FirstSaveToolPath();
        }
        
        FileInfo fileInfo2 = new FileInfo(furniturePath);
        if (fileInfo2.Exists)
        {
            furnitureJsonData = File.ReadAllText(furniturePath);
        }
        else
        {
            FirstSaveFurniturePath();
        }
        toolData = JsonUtility.FromJson<ToolData>(toolJsonData);
        furnitureData = JsonUtility.FromJson<FurnitureData>(furnitureJsonData);
    }

    private static void FirstSaveToolPath()
    {
        toolJsonData = JsonUtility.ToJson(toolData, true);
        File.WriteAllText(toolPath, toolJsonData);
    }
    private static void FirstSaveFurniturePath()
    {
        furnitureJsonData = JsonUtility.ToJson(furnitureData, true);
        File.WriteAllText(furniturePath, furnitureJsonData);
    }
    
    


    public void PassData(int index, string name, SortOfInstall sortOfInstall)
    {
        switch (sortOfInstall)
        {
            case SortOfInstall.Furnitue:
            {
                furnitureData._indexNames.Add(new IndexName(index, name));
                furnitureJsonData = JsonUtility.ToJson(furnitureData, true);
                break;
            }
            case SortOfInstall.Tool:
            {
                toolData._indexNames.Add(new IndexName(index, name));
                toolJsonData = JsonUtility.ToJson(toolData, true);
                break;
            }
        }
    }
    public void SaveData()
    {
        File.WriteAllText(furniturePath, furnitureJsonData);
        File.WriteAllText(toolPath, toolJsonData);
    }

    //public void RestoreHealth(int amount) => health += amount;
    public static void DeleteData(int deleteIndex, SortOfInstall sortOfInstall)
    {
        switch (sortOfInstall)
        {
            case SortOfInstall.Furnitue:
            {
                break;
            }
            case SortOfInstall.Tool:
            {
                for (int i = 0; i < toolData._indexNames.Count; i++)
                {
                    if (toolData._indexNames[i].index == deleteIndex)
                    {
                        toolData._indexNames.RemoveAt(i);
                        toolJsonData = JsonUtility.ToJson(toolData, true);
                    }
                }
                break;
            }
        }
    }
    
}

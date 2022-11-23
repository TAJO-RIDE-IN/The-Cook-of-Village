using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


[Serializable]
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

[Serializable]
public class ChairPositionName
{
    public Vector3 vector3;
    public string name;
    public int tableIndex;

    public ChairPositionName(Vector3 vt3, string b, int index)
    {
        vector3 = vt3;
        name = b;
        tableIndex = index;
    }
}

[Serializable]
public class PositionName
{
    public Vector3 vector3;
    public string name;

    public PositionName(Vector3 vt3, string b)
    {
        vector3 = vt3;
        name = b;
    }
}

/// <summary>
/// 이건 나중에 할거(모든 데이터 하나로 합치는 것)
/// </summary>
[Serializable]
public class InstallObjectData 
{
    public List<IndexName> _toolData = new List<IndexName>();
    public List<Vector3> tableData = new List<Vector3>();
    public List<ChairPositionName> chairData = new List<ChairPositionName>();
    public List<PositionName> furnitureData = new List<PositionName>();
}

[Serializable]
public class ToolData
{
    public List<IndexName> _indexNames = new List<IndexName>();
}

[Serializable]
public class ChairData
{
    public List<ChairPositionName> chairPositionNames = new List<ChairPositionName>();
}

[Serializable]
public class FurnitureData
{
    public List<PositionName> _positionNames = new List<PositionName>();
}
[Serializable]
public class TableData
{
    public List<Vector3> tableVector = new List<Vector3>();
}

public class InstallData : DataManager<InstallData>
{
    protected override void Init()
    {
        LoadData("플레이어 이름_0");
    }

    public override void SaveDataTime(string PlayName)
    {
        SaveData(ref toolData, "ToolData", PlayName);
        SaveData(ref furnitureData, "FurnitureData", PlayName);
        SaveData(ref chairData, "ChairData", PlayName);
        SaveData(ref tableData, "TableData", PlayName);
    }
    public enum SortOfInstall
    {
        Tool,
        Table,
        Chair,
        Furnitue
    };
    
    public ToolData toolData = new ToolData();
    public FurnitureData furnitureData = new FurnitureData();
    public ChairData chairData = new ChairData();
    public TableData tableData = new TableData();

    private static string toolJsonData;
    
    private static string furnitureJsonData;

    public void LoadData(string PlayName)
    {
        Debug.Log("로드완료" + PlayName);
        LoadData(ref toolData, "ToolData", PlayName);
        LoadData(ref furnitureData, "FurnitureData", PlayName);
        LoadData(ref chairData, "ChairData", PlayName);
        LoadData(ref tableData, "TableData", PlayName);
    }


    public void PassVector3Data(SortOfInstall sortOfInstall, Vector3 vt3, string name = "", int tableNumber = 0)
    {
        if (sortOfInstall == SortOfInstall.Chair)
        {
            chairData.chairPositionNames.Add(new ChairPositionName(vt3, name, tableNumber));
        }

        if (sortOfInstall == SortOfInstall.Table)
        {
            tableData.tableVector.Add(vt3);
        }

        if (sortOfInstall == SortOfInstall.Furnitue)
        {
            furnitureData._positionNames.Add(new PositionName(vt3, name));
        }
        
    }

    public void PassIndexData(int index, string name, SortOfInstall sortOfInstall)
    {
        switch (sortOfInstall)
        {
            case SortOfInstall.Tool:
            {
                toolData._indexNames.Add(new IndexName(index, name));
                break;
            }
        }
    }

    //public void RestoreHealth(int amount) => health += amount;
    public void DeleteData(int deleteIndex, SortOfInstall sortOfInstall)
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

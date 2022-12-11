using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public bool isSecond;

    public ChairPositionName(Vector3 vt3, string b, int index, bool isSec = false)
    {
        vector3 = vt3;
        name = b;
        tableIndex = index;
        isSecond = isSec;
    }
}

[Serializable]
public class VecRotName
{
    public Vector3 vector;
    public Vector3 rotation;
    public string name;
    public bool isSecond;

    public VecRotName(Vector3 trans, Vector3 rot ,string b, bool isSec = false)
    {
        vector = trans;
        rotation = rot;
        name = b;
        isSecond = isSec;
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
    public List<ChairPositionName> positionNames = new List<ChairPositionName>();
}

[Serializable]
public class FurnitureData
{
    public List<VecRotName> vecRotNames = new List<VecRotName>();
}
[Serializable]
public class TableData
{
    public List<VecRotName> vectorRotNames = new List<VecRotName>();
}

public class InstallData : DataManager<InstallData>
{

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
        LoadData(ref toolData, "ToolData", PlayName);
        LoadData(ref furnitureData, "FurnitureData", PlayName);
        LoadData(ref chairData, "ChairData", PlayName);
        LoadData(ref tableData, "TableData", PlayName);
    }


    public void PassVector3Data(SortOfInstall sortOfInstall, Vector3 vt3, string name = "", int tableNumber = 0, bool isSecond = false)
    {
        if (sortOfInstall == SortOfInstall.Chair)
        {
            chairData.positionNames.Add(new ChairPositionName(vt3, name, tableNumber, isSecond));
        }

        

    }
    public void PassVecRotData(SortOfInstall sortOfInstall, Vector3 trans, Vector3 rot, string name = "", bool isSecond = false)
    {
        if (sortOfInstall == SortOfInstall.Furnitue)
        {
            furnitureData.vecRotNames.Add(new VecRotName(trans, rot, name, isSecond));
        }
        if (sortOfInstall == SortOfInstall.Table)
        {
            tableData.vectorRotNames.Add(new VecRotName(trans, rot, name, isSecond));
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
    public void DeleteVector3Data(Vector3 deleteVector3, SortOfInstall sortOfInstall)
    {
        switch (sortOfInstall)
        {
            case SortOfInstall.Furnitue:
            {
                for (int i = 0; i < furnitureData.vecRotNames.Count; i++)
                {
                    if (furnitureData.vecRotNames[i].vector == deleteVector3)
                    {
                        furnitureData.vecRotNames.RemoveAt(i);
                    }
                }
                break;
            }
            case SortOfInstall.Table:
            {
                for (int i = 0; i < tableData.vectorRotNames.Count; i++)
                {
                    if (tableData.vectorRotNames[i].vector == deleteVector3)
                    {
                        tableData.vectorRotNames.RemoveAt(i);
                    }
                }
                break;
            }
            case SortOfInstall.Chair:
            {
                for (int i = 0; i < chairData.positionNames.Count; i++)
                {
                    if (chairData.positionNames[i].vector3 == deleteVector3)
                    {
                        chairData.positionNames.RemoveAt(i);
                    }
                }
                break;
            }
        }
    }

    //public void RestoreHealth(int amount) => health += amount;
    public void DeleteIndexData(int deleteIndex, SortOfInstall sortOfInstall)
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
            case SortOfInstall.Table:
            {
                
                break;
            }
            case SortOfInstall.Chair:
            {
                
                break;
            }
        }
    }
    
}

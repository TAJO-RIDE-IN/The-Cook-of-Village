using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();
    public Transform[] toolPosition;
    public GameObject[] toolPositionUI;
    public int availableToolCount;
    private bool[] isUsed;

    private String _selectedToolName;
    public String SelectedToolName
    {
        get
        {
            return _selectedToolName;
        }
        set
        {
            _selectedToolName = value;
        }
    }
    
    [SerializeField] private int _selectedPositionIndex;
    public int SelectedPositionIndex
    {
        get
        {
            return _selectedPositionIndex;
        }
        set
        {
            _selectedPositionIndex = value;
        }
    }

    private void Start()
    {
        isUsed = new bool[availableToolCount];
        SelectedToolName = "Pot";
    }

    public void ReceiveToolIndex(String name)//조리도구 사용한다에 할당
    {
        SelectedToolName = name;
        StartInstall();
    }


    public void ReceivePositionIndex(int x)//UI 클릭에 할당, SelectedToolIndex에 따라서 조리도구 풀해주기
    {
        if (!isUsed[x])
        {
            isUsed[x] = true;
            SelectedPositionIndex = x;
            toolPosition[x].transform.position = new UnityEngine.Vector3(toolPosition[x].transform.position.x,
                FindPoolObjectData(SelectedToolName).installY, toolPosition[x].transform.position.z);
            GetObject(_selectedToolName).transform.position = toolPosition[x].position;
        }
    }

    public void StartInstall()//UI키고, 요리도구 UI 끄기,사용한다에 넣어주기
    {
        for (int i = 0; i < availableToolCount; i++)
        {
            if (!isUsed[i])
            {
                toolPositionUI[i].SetActive(true);
            }
        }
    }

    public void FinishInstall()//UI만 꺼주기
    {
        for (int i = 0; i < availableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
    }

    public void CancelInstall()//풀했던거 돌려주기
    {
        
    }
    
    

}

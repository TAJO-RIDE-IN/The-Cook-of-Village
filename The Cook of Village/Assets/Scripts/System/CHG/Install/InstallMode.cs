using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;



public class InstallMode : MonoBehaviour
{


    public InstallData installData;
    
    public GameObject goInstallUI;
    public GameObject cancelInstallUI;
    public GameObject[] toolPositionUI;
    public CookingTool[] _cookingTools;
    [HideInInspector] public bool isDirectChange;
    
    private List<int> receivedToolPosition = new List<int>();
    private bool isList;
    private bool[] isUsed;
    
    

    public int availableToolCount;
    public InventoryUI inventoryUI;

    private void Start()
    {
        isUsed = new bool[availableToolCount];
        _cookingTools = new CookingTool[availableToolCount];
        
        
        for (int i = 0; i < installData.toolData._indexNames.Count; i++)
        {
            GetAndPosition(installData.toolData._indexNames[i].index, installData.toolData._indexNames[i].name);
        }
    }

    
    

    /// <summary>
    /// 설치할 위치의 인덱스 받아옴.
    /// </summary>
    /// <param name="x"></param>
    public void ReceivePositionIndex(int x)//UI 클릭에 할당
    {
        if (receivedToolPosition.Count == 0)
        {
            receivedToolPosition.Add(x);
            toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
            goInstallUI.SetActive(true);
            return;
        }

        for (int i = 0; i < receivedToolPosition.Count; i++)
        {
            
            if (receivedToolPosition[i] == x)
            {
                isList = false;
                break;
            }
            else if(receivedToolPosition[i] != x)
            {
                isList = true;
            }
        }

        if (isList)
        {
            receivedToolPosition.Add(x);
            toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
            goInstallUI.SetActive(true);
            
        }
        else
        {
            toolPositionUI[x].GetComponent<Image>().color = new Color32(250,250,250, 143);
            receivedToolPosition.Remove(x);
        }
    }

    public void DirectChange()//indexToChange를 여기서 바꾸면 깔끔할텐데
    {
        isDirectChange = true;
        inventoryUI.InventoryState();
        inventoryUI.TabClick(6);
    }

    void GetAndPosition(int index, string name)
    {
        _cookingTools[index] = ToolPooling.Instance.GetObject(name);
        _cookingTools[index].transform.position = ToolPooling.Instance.toolPosition[index].position;
        _cookingTools[index].transform.rotation = ToolPooling.Instance.toolPosition[index].rotation;
    }
    public void UseTool(string name)
    {
        
        if (isDirectChange)
        {
            
            ToolPooling.Instance.ReturnObject(_cookingTools[ToolPooling.Instance.indexToChange],
                _cookingTools[ToolPooling.Instance.indexToChange].type.ToString());
            GetAndPosition(ToolPooling.Instance.indexToChange, name);
            installData.SaveData(ToolPooling.Instance.indexToChange, name);
            isDirectChange = false;
            return;
        }
        else
        {
            StartInstall();
            ToolPooling.Instance.SelectedToolName = name;
        }
    }
    public void GoInstall()//UI만 꺼주기
    {
        foreach (var index in receivedToolPosition)
        {
            if (!isUsed[index])
            {
                isUsed[index] = true;
                ToolPooling.Instance.SelectedPositionIndex = index;
                GetAndPosition(index, ToolPooling.Instance.SelectedToolName);
                installData.SaveData(index, ToolPooling.Instance.SelectedToolName);
                _cookingTools[index].index = index;
            }
        }
        for (int i = 0; i < availableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        isDirectChange = false;
        GameManager.Instance.IsInstall = false;
        receivedToolPosition.Clear();
        //GameManager.Instance.Pause();
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
        GameManager.Instance.IsInstall = true;
        //GameManager.Instance.Pause();
        cancelInstallUI.SetActive(true);
    }

    

    public void CancelInstall()//풀했던거 돌려주기
    {
        //ReturnObject();
        for (int i = 0; i < availableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        isDirectChange = false;
        GameManager.Instance.IsInstall = false;
    }
}

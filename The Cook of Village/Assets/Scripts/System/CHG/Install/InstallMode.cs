using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallMode : MonoBehaviour
{
    public GameObject goInstallUI;
    public GameObject cancelInstallUI;
    
    public GameObject[] toolPositionUI;

    private bool isList;
    private bool[] isUsed;
    private CookingTool[] _cookingTools;
    [HideInInspector] public bool isDirectChange;
    private List<int> receivedToolPosition = new List<int>();
    private Image _image;

    
    public int availableToolCount;
    public InventoryUI inventoryUI;
    
    private void Start()
    {
        isUsed = new bool[availableToolCount];
        _cookingTools = new CookingTool[availableToolCount];
        ToolPooling.Instance.SelectedToolName = "Pot";
    }

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
        Debug.Log("다이렉트");
        isDirectChange = true;
        inventoryUI.InventoryState();
        inventoryUI.TabClick(6);
    }
    public void UseTool(string name)
    {
        if (isDirectChange)
        {
            Debug.Log(_cookingTools[ToolPooling.Instance.indexToChange].type.ToString());
            ToolPooling.Instance.ReturnObject(_cookingTools[ToolPooling.Instance.indexToChange],
                _cookingTools[ToolPooling.Instance.indexToChange].type.ToString());
            _cookingTools[ToolPooling.Instance.indexToChange] = ToolPooling.Instance.GetObject(name);
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
                _cookingTools[index] = ToolPooling.Instance.GetObject(ToolPooling.Instance.SelectedToolName);
                _cookingTools[index].transform.position = ToolPooling.Instance.toolPosition[index].position;
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

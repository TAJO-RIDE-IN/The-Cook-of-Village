using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class InstallMode : MonoBehaviour
{
    public GameObject goInstallUI;
    public GameObject cancelInstallUI;
    public GameObject[] toolPositionUI;
    public CookingTool[] _cookingTools;
    [HideInInspector] public bool isDirectChange;
    
    public List<int> receivedToolPosition = new List<int>();
    /// <summary>
    /// 이 값을 리스트에 넣어도 되는지
    /// </summary>
    private bool isList;
    private bool[] isUsed;
    public int toolItemInfosAmount;
    /// <summary>
    /// 설치하고 싶은 자리의 선택된 개수
    /// </summary>
    public int selectedToolAmount;
    
    

    public int installableToolCount;
    public InventoryUI inventoryUI;

    private void Start()
    {
        isUsed = new bool[installableToolCount];
        _cookingTools = new CookingTool[installableToolCount];
        
        Debug.Log(InstallData.toolData._indexNames.Count);
        
        for (int i = 0; i < InstallData.toolData._indexNames.Count; i++)
        {
            GetAndPosition(InstallData.toolData._indexNames[i].index, InstallData.toolData._indexNames[i].name);
        }
    }

    /// <summary>
    /// 설치할 위치의 인덱스 받아옴.
    /// </summary>
    /// <param name="x"></param>
    public void ReceivePositionIndex(int x)//UI 클릭에 할당
    {
        if (receivedToolPosition.Count == 0)//원래 0이였을 때는 바로 return
        {
            if (toolItemInfosAmount > selectedToolAmount)//데이터에 있는 개수만큼만 UI 활성화
            {
                selectedToolAmount++;
                receivedToolPosition.Add(x);
                toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
                goInstallUI.SetActive(true);
                return;
            }
            else
            {
                //경고 UI
            }
            return;
        }
        for (int i = 0; i < receivedToolPosition.Count; i++)
        {
            
            if (receivedToolPosition[i] == x)//리스트에 새로 받은 값(x)과 같은 게 없다면 빼줘야 하므로 false
            {
                isList = false;
                break;
            }
            else if(receivedToolPosition[i] != x)//리스트에 새로 받은 값(x)과 같은 게 없다면 넣어도 되므로 true
            {
                isList = true;
            }
        }
        if (isList)
        {
            if (toolItemInfosAmount > selectedToolAmount)// = 이라면 이미 개수만큼 다 선택한 것이므로
            {
                selectedToolAmount++;
                receivedToolPosition.Add(x);
                toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
                goInstallUI.SetActive(true);
            }
            else
            {
                //경고UI
            }
        }
        else
        {
            selectedToolAmount--;
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
    public void UseTool(string name, int amount)
    {
        if (isDirectChange)
        {
            ToolPooling.Instance.ReturnObject(_cookingTools[ToolPooling.Instance.indexToChange],
                _cookingTools[ToolPooling.Instance.indexToChange].type.ToString());
            GetAndPosition(ToolPooling.Instance.indexToChange, name);
            InstallData.SaveData(ToolPooling.Instance.indexToChange, name);
            FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolIndex).Amount++;
            isDirectChange = false;
            return;
        }
        else
        {
            toolItemInfosAmount = amount;
            StartInstall();
            ToolPooling.Instance.SelectedToolName = name;
        }
        isDirectChange = false;
    }
    public void DeleteTool(string name)
    {
        if (isDirectChange)
        {
            ReturnTool();
            GetAndPosition(ToolPooling.Instance.indexToChange, name);
            InstallData.SaveData(ToolPooling.Instance.indexToChange, name);
            FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolIndex).Amount++;
            isDirectChange = false;
            return;
        }
        else
        {
            //availableToolAmount = amount;
            StartInstall();
            ToolPooling.Instance.SelectedToolName = name;
        }
        isDirectChange = false;
    }

    private void ReturnTool()
    {
        //_cookingTools[ToolPooling.Instance.indexToChange]
        ToolPooling.Instance.ReturnObject(_cookingTools[ToolPooling.Instance.indexToChange],
            _cookingTools[ToolPooling.Instance.indexToChange].type.ToString());
        
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
                InstallData.SaveData(index, ToolPooling.Instance.SelectedToolName);
                FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolIndex).Amount++;
                _cookingTools[index].index = index;
            }
        }
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolIndex).Amount -= selectedToolAmount - 1;//나은이 함수에서 한번 --해주기 때문. (SlotInventory.UseItem)
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        isDirectChange = false;
        GameManager.Instance.IsInstall = false;
        receivedToolPosition.Clear();
        //GameManager.Instance.Pause();
    }

    
    /// <summary>
    /// UI키고, 요리도구 UI 끄기, 사용한다에 넣을 것
    /// </summary>
    public void StartInstall()
    {
        for (int i = 0; i < installableToolCount; i++)
        {
            if (!isUsed[i])
            {
                toolPositionUI[i].SetActive(true);
            }
            else
            {
                
            }
        }
        ReturnColor();
        GameManager.Instance.IsInstall = true;
        //GameManager.Instance.Pause();
        cancelInstallUI.SetActive(true);
    }

    
    /// <summary>
    /// Direct가 아닐때만 취소할 수 있도록
    /// </summary>
    public void CancelInstall()
    {
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolIndex).Amount++;//SlotInventory.UseItem 에서 -- 해주기 때문
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        ReturnColor();
        isDirectChange = false;
        GameManager.Instance.IsInstall = false;
    }

    private void ReturnColor()
    {
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].GetComponent<Image>().color = new Color32(250,250,250, 143);
        }
    }
}

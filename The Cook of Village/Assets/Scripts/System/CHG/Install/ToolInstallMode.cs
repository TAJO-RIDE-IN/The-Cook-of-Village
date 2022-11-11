using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class ToolInstallMode : InstallMode
{
    public int installableToolCount;

    public GameObject[] PositionParticle;
    
    [HideInInspector] public bool isDirectChange;
    [HideInInspector] public bool isDirectInstall;
    
    [HideInInspector] public bool[] isUsed;


    private void Start()
    {
        isUsed = new bool[installableToolCount];
        
        //Debug.Log(InstallData.toolData._indexNames.Count);
        
        for (int i = 0; i < InstallData.toolData._indexNames.Count; i++)
        {
            GetAndPosition(InstallData.toolData._indexNames[i].index, InstallData.toolData._indexNames[i].name);
        }
    }

    /// <summary>
    /// 설치할 위치의 인덱스 받아옴.
    /// </summary>
    /// <param name="x"></param>
    public override void ReceivePositionIndex(int x)//UI 클릭에 할당
    {
        if (receivedPositionList.Count == 0)//원래 0이였을 때는 바로 return
        {
            if (toolItemInfosAmount > selectedToolAmount)//데이터에 있는 개수만큼만 UI 활성화
            {
                selectedToolAmount++;
                receivedPositionList.Add(x);
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
        for (int i = 0; i < receivedPositionList.Count; i++)
        {
            
            if (receivedPositionList[i] == x)//리스트에 새로 받은 값(x)과 같은 게 없다면 빼줘야 하므로 false
            {
                isList = false;
                break;
            }
            else if(receivedPositionList[i] != x)//리스트에 새로 받은 값(x)과 같은 게 없다면 넣어도 되므로 true
            {
                isList = true;
            }
        }
        if (isList)
        {
            if (toolItemInfosAmount > selectedToolAmount)// = 이라면 이미 개수만큼 다 선택한 것이므로
            {
                selectedToolAmount++;
                receivedPositionList.Add(x);
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
            receivedPositionList.Remove(x);
        }
    }

    public void DirectChange()//indexToChange를 여기서 바꾸면 깔끔할텐데
    {
        isDirectChange = true;
        inventoryUI.InventoryState();
        inventoryUI.TabClick(6);
    }
    
    public void DirectInstall()//indexToChange를 여기서 바꾸면 깔끔할텐데
    {
        isDirectInstall = true;
        inventoryUI.InventoryState();
        inventoryUI.TabClick(6);
    }

    public override void GetAndPosition(int index, string name)
    {
        ToolPooling.Instance.pooledObject[index] = ToolPooling.Instance.GetObject(name);
        ToolPooling.Instance.pooledObject[index].transform.position = ToolPooling.Instance.toolPosition[index].position;
        ToolPooling.Instance.pooledObject[index].transform.rotation = ToolPooling.Instance.toolPosition[index].rotation;
        ToolPooling.Instance.pooledObject[index].index = index;
        isUsed[index] = true;
        PositionParticle[index].SetActive(true);
    }
    public override void Use(string name, int amount)
    {
        if (isDirectChange)
        {
            ToolPooling.Instance.pooledObject[ToolPooling.Instance.indexToChange].DeleteTool();
            Return();
            GetAndPosition(ToolPooling.Instance.indexToChange, name);
            InstallData.SaveData(ToolPooling.Instance.indexToChange, name, InstallData.SortOfInstall.Tool);
            //FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolID).Amount++; => 바꿔주면 해주기
            isDirectChange = false;
            return;
        }
        if (isDirectInstall)
        {
            GetAndPosition(ToolPooling.Instance.indexToChange, name);
            InstallData.SaveData(ToolPooling.Instance.indexToChange, name, InstallData.SortOfInstall.Tool);
            //FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolID).Amount++; => 바꿔주면 해주기
            isDirectInstall = false;
            return;
        }
        toolItemInfosAmount = amount;
        StartInstall();
        ToolPooling.Instance.SelectedToolName = name;
        isDirectChange = false;
    }

    protected override void Return()
    {
        ToolPooling.Instance.ReturnObject(ToolPooling.Instance.pooledObject[ToolPooling.Instance.indexToChange],
            ToolPooling.Instance.pooledObject[ToolPooling.Instance.indexToChange].toolID.ToString());
        
    }
    public override void GoInstall()//UI만 꺼주기
    {
        foreach (var index in receivedPositionList)
        {
            if (! isUsed[index])
            {
                isUsed[index] = true;
                PositionParticle[index].SetActive(true);
                ToolPooling.Instance.SelectedPositionIndex = index;
                GetAndPosition(index, ToolPooling.Instance.SelectedToolName);
                InstallData.SaveData(index, ToolPooling.Instance.SelectedToolName, InstallData.SortOfInstall.Tool);
                FoodData.Instance.FindFoodTool(ToolPooling.Instance.SelectedToolID).Amount++;
                ToolPooling.Instance.pooledObject[index].index = index;
            }
        }
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolID).Amount -= selectedToolAmount - 1;//나은이 함수에서 한번 --해주기 때문. (SlotInventory.UseItem)
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        isDirectChange = false;
        GameManager.Instance.IsInstall = false;
        receivedPositionList.Clear();
        //GameManager.Instance.Pause();
    }

    
    /// <summary>
    /// UI키고, 요리도구 UI 끄기, 사용한다에 넣을 것
    /// </summary>
    protected override void StartInstall()
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
    public override void CancelInstall()
    {
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolID).Amount++;//SlotInventory.UseItem 에서 -- 해주기 때문
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

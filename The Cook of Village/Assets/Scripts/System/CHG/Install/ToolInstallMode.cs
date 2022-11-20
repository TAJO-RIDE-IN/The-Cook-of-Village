using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class ToolInstallMode : InstallMode
{
    public int installableToolCount;
    public InventoryUI inventoryUI;
    public GameObject goInstallUI;
    public GameObject cancelInstallUI;
    public GameObject[] toolPositionUI;
    public GameObject[] PositionCollider;
    public List<int> receivedPositionList = new List<int>();
    //[HideInInspector] 
    public bool isDirectChange;
    //[HideInInspector] 
    public bool isDirectInstall;
    public bool[] isUsed;
    
    private int selectedToolAmount;
    private int toolItemInfosAmount;
    private ToolPooling toolPooling;
    [HideInInspector] public CookingCharacter _cookingCharacter;

    private void Start()
    {
        toolPooling = ToolPooling.Instance;
        isUsed = new bool[installableToolCount];
        
        //Debug.Log(InstallData.toolData._indexNames.Count);
        
        for (int i = 0; i < InstallData.Instance.toolData._indexNames.Count; i++)
        {
            GetAndPosition(InstallData.Instance.toolData._indexNames[i].index, InstallData.Instance.toolData._indexNames[i].name);
        }

        _cookingCharacter = ChefInventory.Instance._cookingCharacter;
    }

    private void PreviewPositionUpdate()
    {
        //if(Physics.Raycast())
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
    
    public void DirectInstall()
    {
        isDirectInstall = true;
        inventoryUI.InventoryState();
        inventoryUI.TabClick(6);
        
    }

    public void GetAndPosition(int index, string name)
    {
        toolPooling.pooledObject[index] = toolPooling.GetObject(name);
        toolPooling.pooledObject[index].transform.position = ToolPooling.Instance.toolPosition[index].position;
        toolPooling.pooledObject[index].transform.rotation = ToolPooling.Instance.toolPosition[index].rotation;
        toolPooling.pooledObject[index].index = index;
        isUsed[index] = true;
        PositionCollider[index].SetActive(false);
    }

    public void ActviePositionCollider(int index)
    {
        PositionCollider[index].SetActive(true);
    }
    public override void Use(ItemInfos itemInfos)
    {
        
        if (isDirectChange)
        {
            if (itemInfos.Name == "Oven")
            {
                //오븐은 특정 자리에만 설치할 수 있습니다! 출력
                return;
            }
            ToolPooling.Instance.pooledObject[ToolPooling.Instance.indexToChange].DeleteTool();
            ReturnPooledObject();
            GetAndPosition(ToolPooling.Instance.indexToChange, itemInfos.Name);
            InstallData.Instance.PassIndexData(ToolPooling.Instance.indexToChange, itemInfos.Name, InstallData.SortOfInstall.Tool);
            DirectUIOnSetting();
            isDirectChange = false;
            return;
        }
        if (isDirectInstall)
        {
            if (itemInfos.Name == "Oven")
            {
                //position index가 6일 때 오븐이 설치되어있지 않다면
                //오븐은 특정 자리에만 설치할 수 있습니다! 출력
                return;
            }
            
            GetAndPosition(toolPooling.indexToChange, itemInfos.Name);
            InstallData.Instance.PassIndexData(ToolPooling.Instance.indexToChange, itemInfos.Name, InstallData.SortOfInstall.Tool);
            DirectUIOnSetting();
            _cookingCharacter.isCookPositionCollider = false;
            _cookingCharacter._cookPosition.CloseUI(0);
            isDirectInstall = false;
            return;
        }

        
        ToolPooling.Instance.selectedItemInfos = itemInfos;
        toolItemInfosAmount = itemInfos.Amount;
        StartInstall();
        ToolPooling.Instance.SelectedToolName = itemInfos.Name;
        isDirectChange = false;
    }

    /// <summary>
    /// InventoryBig 켜지게 하고 isSpace도 true로 해줘서 스페이스바 누르면 바로 UI 꺼지도록
    /// </summary>
    private void DirectUIOnSetting()
    {
        _cookingCharacter._cookingTool = toolPooling.pooledObject[ToolPooling.Instance.indexToChange];
        _cookingCharacter._cookingTool.OpenUI(0);
        _cookingCharacter.isToolCollider = true;
        _cookingCharacter.isSpace = true;
        
    }

    protected override void ReturnPooledObject()
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
                ToolPooling.Instance.SelectedPositionIndex = index;
                GetAndPosition(index, ToolPooling.Instance.SelectedToolName);
                InstallData.Instance.PassIndexData(index, ToolPooling.Instance.SelectedToolName, InstallData.SortOfInstall.Tool);
                ToolPooling.Instance.pooledObject[index].index = index;
            }
        }
        for (int i = 0; i < installableToolCount; i++)
        {
            toolPositionUI[i].SetActive(false);
        }

        ItemData.Instance.ChangeAmount(ToolPooling.Instance.selectedItemInfos.ID,
            ToolPooling.Instance.selectedItemInfos.Amount - (selectedToolAmount - 1));//나은이 함수에서 한번 빼줘서
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

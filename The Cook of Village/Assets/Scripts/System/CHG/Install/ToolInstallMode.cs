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
    public GameObject[] PositionCanvas;
    public List<int> receivedPositionList = new List<int>();
    //[HideInInspector] 
    public bool isDirectChange;
    //[HideInInspector] 
    public bool isDirectInstall;
    [HideInInspector] public bool[] isUsed;
    
    private int selectedToolAmount;
    private int toolItemInfosAmount;
    private ToolPooling toolPooling;
    private InstallData _installData;
    private SoundManager _soundManager;
    public CookingCharacter _cookingCharacter;

    private void Start()
    {
        toolPooling = ToolPooling.Instance;
        _installData = InstallData.Instance;

        _soundManager = SoundManager.Instance;
        isUsed = new bool[installableToolCount];
        
        //Debug.Log(InstallData.toolData._indexNames.Count);
        
        for (int i = 0; i < _installData.toolData._indexNames.Count; i++)
        {
            GetAndPosition(_installData.toolData._indexNames[i].index, InstallData.Instance.toolData._indexNames[i].name);
        }

    }

    private void PreviewPositionUpdate()
    {
        //if(Physics.Raycast())
    }
    
    /// <summary>
    /// ?�치???�치???�덱??받아??
    /// </summary>
    /// <param name="x"></param>
    public override void ReceivePositionIndex(int x)//UI ?�릭???�당
    {
        if (receivedPositionList.Count == 0)//?�래 0?��????�는 바로 return
        {
            if (toolItemInfosAmount > selectedToolAmount)//?�이?�에 ?�는 개수만큼�?UI ?�성??
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
            
            if (receivedPositionList[i] == x)//리스?�에 ?�로 받�? �?x)�?같�? �??�다�?빼줘???��?�?false
            {
                isList = false;
                break;
            }
            else if(receivedPositionList[i] != x)//리스?�에 ?�로 받�? �?x)�?같�? �??�다�??�어???��?�?true
            {
                isList = true;
            }
        }
        if (isList)
        {
            if (toolItemInfosAmount > selectedToolAmount)// = ?�라�??��? 개수만큼 ???�택??것이므�?
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

    public void DirectChange()//indexToChange�??�기??바꾸�?깔끔?�텐??
    {
        isDirectChange = true;
        inventoryUI.InventoryState();
        inventoryUI.ChangeToggle(6);
    }
    
    public void DirectInstall()
    {
        isDirectInstall = true;
        inventoryUI.InventoryState();
        inventoryUI.ChangeToggle(6);
        
    }

    public void GetAndPosition(int index, string name)
    {
        toolPooling.pooledObject[index] = toolPooling.GetObject(name);
        toolPooling.pooledObject[index].transform.position = toolPooling.toolPosition[index].position;
        toolPooling.pooledObject[index].transform.rotation = toolPooling.toolPosition[index].rotation;
        toolPooling.pooledObject[index].index = index;
        isUsed[index] = true;
        PositionCollider[index].SetActive(false);
        PositionCanvas[index].SetActive(false);
    }

    public void ActviePositionCollider(int index)
    {
        PositionCollider[index].SetActive(true);
        PositionCanvas[index].SetActive(false);
    }
    public override void Use(ItemInfos itemInfos)
    {
        if (isDirectChange)
        {
            if (itemInfos.Name == "Oven")
            {
                //?�븐?� ?�정 ?�리?�만 ?�치?????�습?�다! 출력
                return;
            }
            
            toolPooling.pooledObject[toolPooling.indexToChange].DeleteTool();
            ReturnPooledObject();
            GetAndPosition(toolPooling.indexToChange, itemInfos.Name);
            DirectUIOpenSetting();
            InstallData.Instance.PassIndexData(toolPooling.indexToChange, itemInfos.Name, InstallData.SortOfInstall.Tool);
            isDirectChange = false;
            _soundManager.Play(_soundManager._audioClips["Install Cooker02"]);
            return;
        }
        if (isDirectInstall)
        {
            if (itemInfos.Name == "Oven")
            {
                //position index가 6?????�븐???�치?�어?��? ?�다�?
                //?�븐?� ?�정 ?�리?�만 ?�치?????�습?�다! 출력
                return;
            }
            GetAndPosition(toolPooling.indexToChange, itemInfos.Name);
            InstallData.Instance.PassIndexData(toolPooling.indexToChange, itemInfos.Name, InstallData.SortOfInstall.Tool);
            DirectUICloseSetting();
            DirectUIOpenSetting();
            isDirectInstall = false;
            _soundManager.Play(_soundManager._audioClips["Install Cooker02"]);
            return;
        }

        
        toolPooling.selectedItemInfos = itemInfos;
        toolItemInfosAmount = itemInfos.Amount;
        StartInstall();
        toolPooling.SelectedToolName = itemInfos.Name;
        isDirectChange = false;
    }

    public void DirectUICloseSetting()
    {
        _cookingCharacter._cookPosition.isDirect = true;
        _cookingCharacter._cookingTool = toolPooling.pooledObject[_cookingCharacter._cookPosition.index];
        _cookingCharacter._cookPosition.CloseUI(0f);
        _cookingCharacter.isCookPositionCollider = false;
    }

    /// <summary>
    /// InventoryBig 켜�?�??�고 isSpace??true�??�줘???�페?�스�??�르�?바로 UI 꺼�??�록
    /// </summary>
    public void DirectUIOpenSetting()
    {
       
        _cookingCharacter.isToolCollider = true;
        _cookingCharacter.isSpace = true;
        //_cookingCharacter._cookingTool.OpenUI(0.5f);
    }

    protected override void ReturnPooledObject()
    {
        ToolPooling.Instance.ReturnObject(toolPooling.pooledObject[toolPooling.indexToChange],
            ToolPooling.Instance.pooledObject[toolPooling.indexToChange].toolID.ToString());
        
    }
    public override void GoInstall()//UI�?꺼주�?
    {
        if (receivedPositionList.Count > 0)
        {
            foreach (var index in receivedPositionList)
            {
                if (! isUsed[index])
                {
                    isUsed[index] = true;
                    toolPooling.SelectedPositionIndex = index;
                    GetAndPosition(index, toolPooling.SelectedToolName);
                    InstallData.Instance.PassIndexData(index, toolPooling.SelectedToolName, InstallData.SortOfInstall.Tool);
                    toolPooling.pooledObject[index].index = index;
                    PositionCollider[index].SetActive(false);
                    PositionCanvas[index].SetActive(false);
                }
            }
            for (int i = 0; i < installableToolCount -1; i++)
            {
                toolPositionUI[i].SetActive(false);
            }

            ItemData.Instance.ChangeAmount(toolPooling.selectedItemInfos.ID,
                ToolPooling.Instance.selectedItemInfos.Amount - (selectedToolAmount - 1));//?��????�수?�서 ?�번 빼줘??
            toolItemInfosAmount = 0;
            selectedToolAmount = 0;
            cancelInstallUI.SetActive(false);
            goInstallUI.SetActive(false);
            isDirectChange = false;
            receivedPositionList.Clear();
            _soundManager.Play(_soundManager._audioClips["Install Cooker02"]);
        }
    }

    
    /// <summary>
    /// UI?�고, ?�리?�구 UI ?�기, ?�용?�다???�을 �?
    /// </summary>
    protected override void StartInstall()
    {
        for (int i = 0; i < installableToolCount - 1; i++)
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
        //GameManager.Instance.Pause();
        cancelInstallUI.SetActive(true);
    }

    
    /// <summary>
    /// Direct가 ?�닐?�만 취소?????�도�?
    /// </summary>
    public override void CancelInstall()
    {
        for (int i = 0; i < installableToolCount - 1; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolID).Amount++;//SlotInventory.UseItem ?�서 -- ?�주�??�문
        cancelInstallUI.SetActive(false);
        goInstallUI.SetActive(false);
        ReturnColor();
        isDirectChange = false;
    }

    private void ReturnColor()
    {
        for (int i = 0; i < installableToolCount - 1; i++)
        {
            toolPositionUI[i].GetComponent<Image>().color = new Color32(250,250,250, 143);
        }
    }
}

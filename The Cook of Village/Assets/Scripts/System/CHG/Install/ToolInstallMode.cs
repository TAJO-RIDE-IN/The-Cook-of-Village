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
    /// ?§Ïπò???ÑÏπò???∏Îç±??Î∞õÏïÑ??
    /// </summary>
    /// <param name="x"></param>
    public override void ReceivePositionIndex(int x)//UI ?¥Î¶≠???†Îãπ
    {
        if (receivedPositionList.Count == 0)//?êÎûò 0?¥Ï????åÎäî Î∞îÎ°ú return
        {
            if (toolItemInfosAmount > selectedToolAmount)//?∞Ïù¥?∞Ïóê ?àÎäî Í∞úÏàòÎßåÌÅºÎß?UI ?úÏÑ±??
            {
                selectedToolAmount++;
                receivedPositionList.Add(x);
                toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
                goInstallUI.SetActive(true);
                return;
            }
            else
            {
                //Í≤ΩÍ≥† UI
            }
            return;
        }
        for (int i = 0; i < receivedPositionList.Count; i++)
        {
            
            if (receivedPositionList[i] == x)//Î¶¨Ïä§?∏Ïóê ?àÎ°ú Î∞õÏ? Í∞?x)Í≥?Í∞ôÏ? Í≤??ÜÎã§Î©?ÎπºÏ§ò???òÎ?Î°?false
            {
                isList = false;
                break;
            }
            else if(receivedPositionList[i] != x)//Î¶¨Ïä§?∏Ïóê ?àÎ°ú Î∞õÏ? Í∞?x)Í≥?Í∞ôÏ? Í≤??ÜÎã§Î©??£Ïñ¥???òÎ?Î°?true
            {
                isList = true;
            }
        }
        if (isList)
        {
            if (toolItemInfosAmount > selectedToolAmount)// = ?¥ÎùºÎ©??¥Î? Í∞úÏàòÎßåÌÅº ???†ÌÉù??Í≤ÉÏù¥ÎØÄÎ°?
            {
                selectedToolAmount++;
                receivedPositionList.Add(x);
                toolPositionUI[x].GetComponent<Image>().color = new Color32(40,40,40, 143);
                goInstallUI.SetActive(true);
            }
            else
            {
                //Í≤ΩÍ≥†UI
            }
        }
        else
        {
            selectedToolAmount--;
            toolPositionUI[x].GetComponent<Image>().color = new Color32(250,250,250, 143);
            receivedPositionList.Remove(x);
        }
    }

    public void DirectChange()//indexToChangeÎ•??¨Í∏∞??Î∞îÍæ∏Î©?ÍπîÎÅî?†ÌÖê??
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
                //?§Î∏ê?Ä ?πÏ†ï ?êÎ¶¨?êÎßå ?§Ïπò?????àÏäµ?àÎã§! Ï∂úÎ†•
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
                //position indexÍ∞Ä 6?????§Î∏ê???§Ïπò?òÏñ¥?àÏ? ?äÎã§Î©?
                //?§Î∏ê?Ä ?πÏ†ï ?êÎ¶¨?êÎßå ?§Ïπò?????àÏäµ?àÎã§! Ï∂úÎ†•
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
    /// InventoryBig ÏºúÏ?Í≤??òÍ≥† isSpace??trueÎ°??¥Ï§ò???§Ìéò?¥Ïä§Î∞??ÑÎ•¥Î©?Î∞îÎ°ú UI Í∫ºÏ??ÑÎ°ù
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
    public override void GoInstall()//UIÎß?Í∫ºÏ£ºÍ∏?
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
                ToolPooling.Instance.selectedItemInfos.Amount - (selectedToolAmount - 1));//?òÏ????®Ïàò?êÏÑú ?úÎ≤à ÎπºÏ§ò??
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
    /// UI?§Í≥†, ?îÎ¶¨?ÑÍµ¨ UI ?ÑÍ∏∞, ?¨Ïö©?úÎã§???£ÏùÑ Í≤?
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
    /// DirectÍ∞Ä ?ÑÎãê?åÎßå Ï∑®ÏÜå?????àÎèÑÎ°?
    /// </summary>
    public override void CancelInstall()
    {
        for (int i = 0; i < installableToolCount - 1; i++)
        {
            toolPositionUI[i].SetActive(false);
        }
        toolItemInfosAmount = 0;
        selectedToolAmount = 0;
        ItemData.Instance.ItemInfos(ToolPooling.Instance.SelectedToolID).Amount++;//SlotInventory.UseItem ?êÏÑú -- ?¥Ï£ºÍ∏??åÎ¨∏
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

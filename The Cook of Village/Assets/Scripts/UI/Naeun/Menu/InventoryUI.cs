using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UIController
{
    [SerializeField] private ItemType.Type tab;
    public ItemType.Type CurrentTab
    {
        get { return tab; }
        set
        {
            tab = value;
            ResetInventory();
            LoadInventorySlot();
        }
    }
    public ToggleControl toggleControl;
    public Toggle[] TabToggle;
    [SerializeField] private SlotInventory[] slotInventory;
    [SerializeField] private GameObject ItemExplanation;
    private ToolPooling toolPooling;
    private ItemData itemData;
    private Dictionary<int, Toggle> ToggleIndex = new Dictionary<int, Toggle>();
    private void AddToggleDic()
    {
        ToggleIndex.Add(1, TabToggle[0]);
        ToggleIndex.Add(5, TabToggle[1]);
        ToggleIndex.Add(6, TabToggle[2]);
        ToggleIndex.Add(7, TabToggle[3]);
    }
    private void Awake()
    {
        AddToggleDic();
    }
    public void TabClick(int _tab)
    {
        CurrentTab = (ItemType.Type)_tab;
    }

    public void InstallItemTab()
    {
        InventoryState();
        TabClick(7);
    }

    public void InventoryState()
    {
        itemData = ItemData.Instance;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
        {
            CurrentTab = ItemType.Type.Fruit;
            LoadInventorySlot();           
        }
        else
        {
            if(ToolPooling.Instance != null)
            {
                toolPooling = ToolPooling.Instance;
                if (toolPooling.toolInstallMode.isDirectInstall)
                {
                    toolPooling.toolInstallMode.isDirectInstall = false;
                }
            }
        }
        toggleControl.ResetToggle(1);
    }

    private void ResetInventory()
    {
        ItemExplanation.SetActive(false);
        foreach (var slot in slotInventory)
        {
            slot.ResetSlot();
        }
    }

    public void LoadInventorySlot()
    {
        ResetInventory();
        int slotIndex = 0;
        List <ItemInfos> _iteminfos = itemData.ItemType[(int)CurrentTab].ItemInfos;
        if((int)tab == 1)
        {
            _iteminfos = itemData.IngredientList();
        }
        foreach (var infos in _iteminfos.Select((value, index) => (value, index)))
        {
            if (infos.value.Amount != 0)
            {
                slotInventory[slotIndex].ItemInfos = infos.value;
                slotIndex++;
            }
        }
    }
}

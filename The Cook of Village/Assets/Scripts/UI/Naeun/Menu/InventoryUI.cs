using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UIController
{
    [SerializeField] private SlotInventory[] slotInventory;
    [SerializeField]
    private ItemType.Type tab;
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
    [SerializeField] private GameObject ItemExplanation;
    public ToggleControl toggleControl;

    public void TabClick(int _tab)
    {
        CurrentTab = (ItemType.Type)_tab;
    }

    public void InventoryState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
        {
            CurrentTab = ItemType.Type.Fruit;
            LoadInventorySlot();
            
        }
        else
        {
            if (ToolPooling.Instance.toolInstallMode.isDirectInstall)
            {
                Debug.Log("닫으면 false");
                ToolPooling.Instance.toolInstallMode.isDirectInstall = false;
            }
        }
        toggleControl.ResetToggle(1);
    }
    public void InventoryStateBool(bool state)
    {
        this.gameObject.SetActive(state);
        if (state)
        {
            
            CurrentTab = ItemType.Type.Fruit;
            LoadInventorySlot();
        }
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
        List <ItemInfos> _iteminfos = ItemData.Instance.ItemType[(int)CurrentTab].ItemInfos;
        if((int)tab == 1)
        {
            _iteminfos = ItemData.Instance.IngredientList();
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

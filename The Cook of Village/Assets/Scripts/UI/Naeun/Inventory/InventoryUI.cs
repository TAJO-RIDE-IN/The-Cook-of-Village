using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private InventoryType.Type tab;
    public InventoryType.Type CurrentTab
    {
        get { return tab; }
        set 
        { 
            tab = value;
            LoadInventorySlot();
        }
    }

    [SerializeField]
    private SlotInventory[] slotInventory;
    private void OnEnable()
    {
        LoadInventorySlot();
    }

    public void TabClick(int _tab)
    {
        CurrentTab = (InventoryType.Type)_tab;
    }

    private void LoadInventorySlot()
    {
        List <InventoryItemInfos> Iteminfos = InventoryData.Instance.inventoryType[(int)CurrentTab].InventoryInfos;
        foreach (var infos in Iteminfos.Select((value, index) => (value, index)))
        {
            slotInventory[infos.index].ItemInfos = infos.value;
        }
    }

}

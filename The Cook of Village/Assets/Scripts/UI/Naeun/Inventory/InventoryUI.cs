using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private SlotInventory[] slotInventory;
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
    private void OnEnable()
    {
        LoadInventorySlot();
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

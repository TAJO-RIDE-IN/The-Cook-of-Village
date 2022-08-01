using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
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

    [SerializeField]
    private SlotInventory[] slotInventory;
    private void OnEnable()
    {
        ResetInventory();
        LoadInventorySlot();
    }

    public void TabClick(int _tab)
    {
        CurrentTab = (ItemType.Type)_tab;
    }

    private void ResetInventory()
    {
        foreach (var slot in slotInventory)
        {
            slot.ResetSlot();
        }
    }

    private void LoadInventorySlot()
    {
        List <ItemInfos> Iteminfos = ItemData.Instance.ItemType[(int)CurrentTab].ItemInfos;
        foreach (var infos in Iteminfos.Select((value, index) => (value, index)))
        {
            if (infos.value.Amount != 0)
            {
                slotInventory[infos.index].ItemInfos = infos.value;
            }
        }
    }

}

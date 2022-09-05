using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject ItemExplanation;
    [SerializeField]
    private SlotInventory[] slotInventory;
    private void OnEnable()
    {
        CurrentTab = ItemType.Type.Fruit;
        ResetInventory();
        LoadInventorySlot();
    }

    public void TabClick(int _tab)
    {
        CurrentTab = (ItemType.Type)_tab;
    }

    public void InventoryButton()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        GameManager.Instance.IsUI = this.gameObject.activeSelf;
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

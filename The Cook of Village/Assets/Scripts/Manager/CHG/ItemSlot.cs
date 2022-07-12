using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    private int index;

    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }

    private UnityEngine.UI.Image slotUI;
    private InventoryManager _inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        _inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    public void SlotClick()
    {
        _inventoryManager.SendItem(index);
    }

    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItemSlot : ItemSlot
{
    private void Awake()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }
    
    public override void SlotClick()
    {
        InventoryManager.Instance.SendItem(index);
    }
}

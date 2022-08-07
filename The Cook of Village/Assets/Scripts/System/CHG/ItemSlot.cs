using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] protected int index;

    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }
    protected UnityEngine.UI.Image slotUI;

    public virtual void SlotClick() { }


    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

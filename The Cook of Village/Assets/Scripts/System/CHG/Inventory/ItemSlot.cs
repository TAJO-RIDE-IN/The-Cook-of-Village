using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    protected int index;//?΄κ±΄ ?μ ?΄λ?€μ???¬μ©, Index ?Έλ??μ ?¬μ©

    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }
    public UnityEngine.UI.Image slotUI;
    [HideInInspector] public UnityEngine.UI.Image slotUI2;

    public virtual void SlotClick() { }



    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

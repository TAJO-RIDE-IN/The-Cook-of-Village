using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    protected int index;//이건 상속 클래스에서 사용, Index 외부에서 사용

    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }
    [HideInInspector] public UnityEngine.UI.Image slotUI;
    [HideInInspector] public UnityEngine.UI.Image slotUI2;

    public virtual void SlotClick() { }



    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

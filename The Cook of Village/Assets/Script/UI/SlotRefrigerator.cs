/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRefrigerator : Slot
{
    public int SlotCount
    {
        get { return slotCount; }
        set
        {
            slotCount = value;
            ModifySlot();
            SlotState();
            if(Data != null)
            { 
                Data.ChangeAmount(Type, ID, slotCount);
            }
        }
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        bool state;
        state = (slotCount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
    }
    public override void SelectSlot()
    {
        if (slotCount > 0)
        {
            SlotCount--;
        }
    }
}

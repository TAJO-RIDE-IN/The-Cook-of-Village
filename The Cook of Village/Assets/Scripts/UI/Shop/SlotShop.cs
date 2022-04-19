/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : Slot
{
    public Text SlotText;
    public Text PriceText;
    public Image SlotImage;
    public ShopSelect SelectSlotObject;
    public int SlotCount
    {
        get { return materialInfos.Amount; }
        set { materialInfos.Amount = value; }
    }

    private void OnEnable()
    {
        ModifySlot();
    }
    private void OnDisable()
    {
        materialInfos = null;
    }
    public override void SelectSlot()
    {
        SelectSlotObject.gameObject.SetActive(true);
        SelectSlotObject.slot = this;
        SelectSlotObject.CurrentCount = materialInfos.Amount;
        SelectSlotObject.ModifySlot(materialInfos.Name, materialInfos.ImageUI);
    }

    public override void ModifySlot()
    {
        PriceText.text = materialInfos.Price.ToString();
        SlotImage.sprite = materialInfos.ImageUI;
        SlotText.text = materialInfos.Name;
    }
}

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
    public int Price;
    public string Name;
    public Text SlotText;
    public Text PriceText;
    public Image SlotImage;
    public Image SlotImageData;
    public GameObject SelectSlotObject;
    public int SlotCount
    {
        get { return slotCount; }
        set { slotCount = value; }
    }

    private void OnEnable()
    {
        ModifySlot();
    }

    public override void SelectSlot()
    {
        SelectSlotObject.SetActive(true);
        SelectSlotObject.transform.GetComponent<ShopSelect>().Price = Price;
        SelectSlotObject.transform.GetComponent<ShopSelect>().CountSlider.value = 0;
    }

    public override void ModifySlot()
    {
        CountText.text = slotCount.ToString();
        PriceText.text = Price.ToString();
        //SlotImage = SlotImageData;
        //SlotText.text = Name;
    }
}

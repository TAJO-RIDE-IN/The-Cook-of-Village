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

    private void OnEnable()
    {
        ModifySlot();
    }
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
        ingredientsInfos = null;
    }
    public override void SelectSlot()
    {
        SelectSlotObject.gameObject.SetActive(true);
        SelectSlotObject.slot = this;
        SelectSlotObject.CurrentCount = ingredientsInfos.Amount;
        SelectSlotObject.ModifySlot(ingredientsInfos.Name, ingredientsInfos.ImageUI);
    }

    public override void ModifySlot()
    {
        PriceText.text = ingredientsInfos.Price.ToString();
        SlotImage.sprite = ingredientsInfos.ImageUI;
        SlotText.text = Localization.GetLocalizedString("Ingredient", ingredientsInfos.Name);
    }
}

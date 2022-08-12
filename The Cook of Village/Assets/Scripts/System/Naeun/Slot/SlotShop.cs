using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : Slot<ItemInfos>
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
        Infos = null;
    }
    public override void SelectSlot()
    {
        SelectSlotObject.gameObject.SetActive(true);
        SelectSlotObject.shop = this;
        SelectSlotObject.CurrentCount = Infos.Amount;
        SelectSlotObject.ModifySlot(Localization.GetLocalizedString("Ingredient", Infos.Name), Infos.ImageUI);
    }

    public override void ModifySlot()
    {
        PriceText.text = Infos.Price.ToString();
        SlotImage.sprite = Infos.ImageUI;
        SlotText.text = Localization.GetLocalizedString("Ingredient", Infos.Name);
    }
}

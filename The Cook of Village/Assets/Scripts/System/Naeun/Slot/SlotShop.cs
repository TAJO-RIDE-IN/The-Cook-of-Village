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
        SelectSlotObject.Infos = Infos;
    }

    public override void ModifySlot()
    {
        SlotText.text = Localization.GetLocalizedString("Ingredient", Infos.Name);
        SlotImage.sprite = Infos.ImageUI;
        PriceText.text = Infos.Price.ToString();
    }
}

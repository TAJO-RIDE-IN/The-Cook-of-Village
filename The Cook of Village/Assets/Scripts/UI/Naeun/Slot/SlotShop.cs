using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : Slot<ItemInfos>
{
    public ItemInfos itemInfos
    {
        get { return Infos; }
        set 
        { 
            Infos = value;
            ModifySlot();
        }
    }
    public Text SlotText;
    public Text PriceText;
    public Image SlotImage;
    public ShopSelect SelectSlotObject;

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
        string _text = Localization.GetLocalizedString("Ingredient", Infos.Name);
        SlotText.fontSize = (_text.Length > 7) ? 18 : 22;
        SlotText.text = _text;
        SlotImage.sprite = Infos.ImageUI;
        string price = Infos.Price.ToString();
        PriceText.text = (Infos.Price > GameData.Instance.Money)? "<color=#ff0000>" + price + "</color>" : " <color=#000000ff>" + price + "</color>";
    }
}

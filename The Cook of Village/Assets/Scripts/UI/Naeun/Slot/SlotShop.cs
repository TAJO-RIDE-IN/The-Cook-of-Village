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
            this.gameObject.SetActive(true);
            ModifySlot();
        }
    }
    public Text SlotText;
    public Text PriceText;
    public Image SlotImage;
    public Image SlotBackground;
    public ShopSelect SelectSlotObject;
    public GameObject SoldOut;
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
        SlotImage.sprite = Infos.ImageUI;
        int count = Infos.ShopCount - ShopCount.ShopCountDictionary[Infos.Name];
        string price = Infos.Price.ToString();
        string _name = Infos.KoreanName;
        SlotText.fontSize = (_name.Length > 7) ? 18 : 22;
        SlotText.text = _name;
        SoldOut.gameObject.SetActive(count == 0);
        PriceText.text = (Infos.Price > MoneyData.Instance.Money)? "<color=#ff0000>" + price + "</color>" : " <color=#000000ff>" + price + "</color>";
    }
}

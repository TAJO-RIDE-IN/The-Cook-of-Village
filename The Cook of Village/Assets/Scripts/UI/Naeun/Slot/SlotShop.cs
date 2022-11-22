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
    public int ModifyPrice;
    public Text SlotText;
    public Text PriceText;
    public Image SlotImage;
    public Image SlotBackground;
    public ShopSelect SelectSlotObject;
    public GameObject SoldOut;
    [HideInInspector] public ShopUI shopUI;
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
        Infos = null;
    }
    public override void SelectSlot()
    {
        SelectSlotObject.ModifyPrice = ModifyPrice;
        SelectSlotObject.Infos = Infos;
    }

    public override void ModifySlot()
    {
        SlotImage.sprite = ImageData.Instance.FindImageData(Infos.ImageID);

        string _name = Infos.KoreanName;
        if(Infos.type.Equals(ItemType.Type.Furniture))//도구 파는 상점의 ui가 조금 작기 때문에 폰트 크기 조절
        {
            SlotText.fontSize = (_name.Length > 7) ? 16 : 22;
        }
        SlotText.text = _name;
        ChangePrice();
    }

    private void ChangePrice()
    {
        int priceInt = (shopUI.npcWork.Equals(NPCInfos.Work.ChocolateShop)) ? SelectSlotObject.totalprice : ModifyPrice;
        string price = priceInt.ToString();
        int count = Infos.ShopCount - Infos.PurchasesCount;
        if (shopUI.Type.Equals(ShopUI.ShopType.Buy)) //아이템을 사는 경우에만 색을 빨간색으로 바꿀 수 있음
        {
            SoldOut.gameObject.SetActive(count.Equals(0));
            PriceText.text = (ModifyPrice > MoneyData.Instance.Money) ? " <color=#ff0000>" + price + "</color>" : " <color=#000000ff>" + price + "</color>";
        }
        else
        {
            SoldOut.gameObject.SetActive(false);
            PriceText.text = " " + "<color=#000000ff>" + price + "</color>";
        }
    }
}

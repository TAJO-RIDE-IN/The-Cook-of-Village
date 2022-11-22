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
        if(Infos.type.Equals(ItemType.Type.Furniture))//���� �Ĵ� ������ ui�� ���� �۱� ������ ��Ʈ ũ�� ����
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
        if (shopUI.Type.Equals(ShopUI.ShopType.Buy)) //�������� ��� ��쿡�� ���� ���������� �ٲ� �� ����
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

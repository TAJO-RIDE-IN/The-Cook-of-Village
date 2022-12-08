using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSelect : MonoBehaviour
{
    [SerializeField]
    private ItemInfos infos;
    public ItemInfos Infos
    {
        get{ return infos;  }
        set
        {
            infos = value;
            if(ExplanationText != null)
            {
                ExplanationText.text = Infos.Explanation;
            }
            Init();
        }
    }
    [HideInInspector]public int ModifyPrice;
    public Text TotalPrice;
    public Text CountText;
    public Text NameText;
    public Text ExplanationText;
    public Image SelectImage;
    public Slider CountSlider;
    public ShopNPC NPC;
    public ShopUI shopUI;

    [HideInInspector]public int totalprice;
    private int BuyMaxCount;
    private int MoneyMaxCount;

    private void Init()
    {
        BuyMaxCount = (shopUI.Type.Equals(ShopUI.ShopType.Buy)) ? Infos.ShopCount - Infos.PurchasesCount : Infos.Amount;
        MoneyMaxCount = (int)MoneyData.Instance.Money / Infos.Price;
        ModifySlot();
        ChangeSelctText();
        totalprice = 0;
        CountSlider.value = 0;
        CountSlider.maxValue = BuyMaxCount;
    }

    private int CalculatePrice(int count, int Price)
    {
        int price = (int) (count * Price);
        return price;
    }
    private int CurrentAmount()
    {

        int amount = (int)CountSlider.value;
        amount = (shopUI.Type.Equals(ShopUI.ShopType.Buy)) ? amount : -amount;
        return amount;
    }
    public void ModifySlot()
    {
        NameText.fontSize = (Infos.KoreanName.Length > 7) ? 18 : 23;
        NameText.text = Infos.KoreanName;
        ExplanationText.text = infos.Explanation;
        SelectImage.sprite = ImageData.Instance.FindImageData(Infos.ImageID);
    }
    public void ChangeSelctText()
    {
        CountText.text = CountSlider.value.ToString() + " 개";
        TotalPrice.text = CalculatePrice((int)CountSlider.value, ModifyPrice).ToString();
        totalprice = Int32.Parse(TotalPrice.text);
        if (CountSlider.value > BuyMaxCount)
        {
            CountSlider.value = BuyMaxCount;
        }
        if (CountSlider.value > MoneyMaxCount && shopUI.Type == ShopUI.ShopType.Buy)
        {
            CountSlider.value = MoneyMaxCount;
        }
    }
    public void BuyMaterial()
    {
        if (CountSlider.value != 0)
        {
            if (shopUI.Type.Equals(ShopUI.ShopType.Buy)) //아이템 구매
            {
                NPCData.Instance.ChangeLikeability(NPC.npcInfos.work, "PlayerUse");
                Infos.PurchasesCount += (int)CountSlider.value;
                totalprice = totalprice * -1;
                MoneyData.Instance.Money += totalprice; //돈 - 해줌
            }
            else //아이템 판매
            {
                MoneyData.Instance.ChangeProceedsData(MoneyData.ProceedDataType.Resell, totalprice);
            }
            NPC.CurrentState = ShopNPC.State.Sell;
            ItemData.Instance.ChangeAmount(Infos.ID, CurrentAmount());
            shopUI.LoadSlotData();
            Init();
        }
    }
}

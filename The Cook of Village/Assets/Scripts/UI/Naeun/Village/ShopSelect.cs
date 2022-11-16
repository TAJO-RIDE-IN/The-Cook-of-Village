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
    
    private int BuyMaxCount;
    private int MoneyMaxCount;

    private void Init()
    {
        BuyMaxCount = (shopUI.Type == ShopUI.ShopType.Buy) ? Infos.ShopCount - Infos.PurchasesCount : Infos.Amount;
        MoneyMaxCount = (int)MoneyData.Instance.Money / Infos.Price;
        ModifySlot(Infos.KoreanName, Infos.ImageUI);
        ChangeSelctText();
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
        amount = (shopUI.Type == ShopUI.ShopType.Buy) ? amount : -amount;
        return amount;
    }
    public void ModifySlot(string name, Sprite slotImage)
    {
        NameText.fontSize = (name.Length > 7) ? 18 : 23;
        NameText.text = name;
        ExplanationText.text = infos.Explanation;
        SelectImage.sprite = slotImage;
    }
    public void ChangeSelctText()
    {
        CountText.text = CountSlider.value.ToString() + " °³";
        TotalPrice.text = CalculatePrice((int)CountSlider.value, ModifyPrice).ToString();
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
            int totalprice = Int32.Parse(TotalPrice.text);
            if (shopUI.Type == ShopUI.ShopType.Buy)
            {
                NPCData.Instance.ChangeLikeability(NPC.npcInfos.work, "PlayerUse");
                Infos.PurchasesCount += (int)CountSlider.value;
                totalprice = totalprice * -1;
            }
            NPC.CurrentState = ShopNPC.State.Sell;
            ItemData.Instance.ChangeAmount(Infos.ID, CurrentAmount());
            MoneyData.Instance.Money += totalprice;
            shopUI.LoadSlotData();
            Init();
        }
    }
}

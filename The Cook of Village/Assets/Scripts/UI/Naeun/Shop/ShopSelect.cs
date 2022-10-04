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
/*    private void OnDisable()
    {
        CloseSelcetSlot();
    }*/
    private void Init()
    {
        BuyMaxCount = Infos.ShopCount - ShopCount.ShopCountDictionary[Infos.Name];
        MoneyMaxCount = (int)GameData.Instance.Money / Infos.Price;
        ModifySlot(Localization.GetLocalizedString("Ingredient", Infos.Name), Infos.ImageUI);
        CountSlider.value = 0;
        CountSlider.maxValue = BuyMaxCount;
    }
    public void CloseSelcetSlot()
    {
        this.gameObject.SetActive(false);
    }
    private int CalculatePrice(int count, int Price)
    {
        int price = count * Price;
        return price;
    }
    private int CurrentAmount()
    {
        int amount;
        amount = infos.Amount + Int32.Parse(CountText.text); ;
        return amount;
    }
    public void ModifySlot(string name, Sprite slotImage)
    {
        NameText.fontSize = (name.Length > 7) ? 18 : 23;
        NameText.text = name;
        SelectImage.sprite = slotImage;
    }
    public void ChangeSelctText()
    {
        CountText.text = CountSlider.value.ToString();
        TotalPrice.text = CalculatePrice((int)CountSlider.value, Infos.Price).ToString();
        if (CountSlider.value > BuyMaxCount)
        {
            CountSlider.value = BuyMaxCount;
        }
        if (CountSlider.value > MoneyMaxCount)
        {
            CountSlider.value = MoneyMaxCount;
        }
    }
    public void BuyMaterial()
    {
        if (CountSlider.value != 0)
        {
            NPC.CurrentState = ShopNPC.State.Sell;
            ShopCount.ShopCountDictionary[Infos.Name] += (int)CountSlider.value;
            ItemData.Instance.ChangeAmount(Infos.ID, CurrentAmount());
            GameData.Instance.Money -= Int32.Parse(TotalPrice.text);
            shopUI.LoadSlotData();
            Init();
        }
    }
}

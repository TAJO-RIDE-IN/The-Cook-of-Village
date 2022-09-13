using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSelect : MonoBehaviour
{
    public Text TotalPrice;
    public Text CountText;
    public Text NameText;
    public Image SelectImage;
    public Slider CountSlider;
    public SlotShop shop;
    public ShopNPC NPC;
    private int currentCount;

    public int CurrentCount
    {
        get { return currentCount; }
        set
        {
            currentCount = value;
            BuyMaxCount = shop.Infos.ShopCount - ShopCount.ShopCountDictionary[shop.Infos.Name];
            CountSlider.value = 0;
            CountSlider.maxValue = BuyMaxCount;
            MoneyMaxCount = (int)GameData.Instance.Money / shop.Infos.Price;
        }
    }
    
    private int BuyMaxCount;
    private int MoneyMaxCount;
    private void OnDisable()
    {
        CloseSelcetSlot();
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
        amount = CurrentCount + Int32.Parse(CountText.text); ;
        return amount;
    }
    public void ModifySlot(string name, Sprite slotImage)
    {
        NameText.text = name;
        SelectImage.sprite = slotImage;
    }
    public void ChangeSelctText()
    {
        CountText.text = CountSlider.value.ToString();
        TotalPrice.text = CalculatePrice((int)CountSlider.value, shop.Infos.Price).ToString();
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
            ShopCount.ShopCountDictionary[shop.Infos.Name] += (int)CountSlider.value;
            ItemData.Instance.ChangeAmount(shop.Infos.ID, CurrentAmount());
            GameData.Instance.Money -= Int32.Parse(TotalPrice.text);
            CurrentCount = CurrentAmount();
        }
    }
}

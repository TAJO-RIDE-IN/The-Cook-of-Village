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
    private int currentCount;
    private GameObject[] NPC;
    public int CurrentCount
    {
        get { return currentCount; }
        set
        {
            currentCount = value;
            CountSlider.value = 0;
            Debug.Log(ShopCount.ShopCountDictionary[shop.Infos.Name]);
            Debug.Log(shop.Infos.ShopCount);
            CountSlider.maxValue = shop.Infos.ShopCount - ShopCount.ShopCountDictionary[shop.Infos.Name];
            BuyMaxCount = ItemData.Instance.MaxMaterialCount - currentCount;
            MoneyMaxCount = (int)GameData.Instance.Money / shop.Infos.Price;
        }
    }
    
    private int BuyMaxCount;
    private int MoneyMaxCount;
    private void Start()
    {
        NPC = GameObject.FindGameObjectsWithTag("Shop");
    }
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
        foreach(GameObject Shop in NPC)
        {
            ShopNPC npc = Shop.GetComponent<ShopNPC>();
            if (npc.isOpen)
            {
                npc.CurrentState = ShopNPC.State.Sell;
            }
        }
        ShopCount.ShopCountDictionary[shop.Infos.Name] += (int)CountSlider.value;
        ItemData.Instance.ChangeAmount(shop.Infos.ID, CurrentAmount());
        GameData.Instance.Money -= Int32.Parse(TotalPrice.text);
        CurrentCount = CurrentAmount();
    }
}

/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
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
    public SlotShop slot;
    private int currentCount;
    public int CurrentCount
    {
        get { return currentCount; }
        set
        {
            currentCount = value;
            CountSlider.value = 0;
            CountSlider.maxValue = MaterialData.Instance.MaxMaterialCount;
            BuyMaxCount = MaterialData.Instance.MaxMaterialCount - currentCount;
            MoneyMaxCount = GameManager.Instance.Money / slot.materialInfos.Price;
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
        TotalPrice.text = CalculatePrice((int)CountSlider.value, slot.materialInfos.Price).ToString();
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
        MaterialData.Instance.ChangeAmount(slot.materialInfos.Type, slot.materialInfos.ID, CurrentAmount());
        GameManager.Instance.Money -= Int32.Parse(TotalPrice.text);
        CurrentCount = CurrentAmount();
        slot.SlotCount = CurrentCount;
    }
}

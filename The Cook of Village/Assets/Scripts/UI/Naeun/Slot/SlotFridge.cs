using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFridge : Slot<ItemInfos>
{
    public Button slot;
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
    public Image BasketImage;
    public Sprite NoneBasketImage;
    public Sprite UseBasketImage;
    private Transform player;
    public FridgeUI FridgeUI;
    public int SlotCount
    {
        get { return Infos.Amount; }
        set
        {
            Infos.Amount = value;
            ModifySlot();
            SlotState();
            ItemData.Instance.ChangeAmount(Infos.ID, Infos.Amount);
        }
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        BasketImage.sprite = (Infos.Amount > 0) ? UseBasketImage : NoneBasketImage;
        IngredientImage.gameObject.SetActive(Infos.Amount > 0);
        slot.interactable = (Infos.Amount > 0);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
        IngredientName.text = Infos.KoreanName;
        IngredientImage.sprite = Infos.ImageUI;
    }
    public override void SelectSlot()
    {
        if (Infos.Amount > 0)
        {
            if (ChefInventory.Instance.AddIngredient(Infos))
            {
                SlotCount--;
            }
        }
    }
}

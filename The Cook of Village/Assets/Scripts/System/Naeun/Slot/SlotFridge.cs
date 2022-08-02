using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFridge : Slot<ItemInfos>
{
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
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
        bool state;
        state = (Infos.Amount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
        IngredientName.text = Localization.GetLocalizedString("Ingredient", Infos.Name);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFridge : Slot
{
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
    private Transform player;
    public FridgeUI FridgeUI;
    public int SlotCount
    {
        get { return ingredientsInfos.Amount; }
        set
        {
            ingredientsInfos.Amount = value;
            ModifySlot();
            SlotState();
            IngredientsData.Instance.ChangeAmount(ingredientsInfos.Type, ingredientsInfos.ID, ingredientsInfos.Amount);
        }
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        bool state;
        state = (ingredientsInfos.Amount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
        IngredientName.text = Localization.GetLocalizedString("Ingredient", ingredientsInfos.Name);
        IngredientImage.sprite = ingredientsInfos.ImageUI;
    }
    public override void SelectSlot()
    {
        if (ingredientsInfos.Amount > 0)
        {
            if (InventoryManager.Instance.AddIngredient(ingredientsInfos))
            {
                SlotCount--;
            }
        }
    }
}

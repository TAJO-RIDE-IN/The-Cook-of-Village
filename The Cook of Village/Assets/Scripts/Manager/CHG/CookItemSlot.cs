using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItemSlot : ItemSlot
{
    public enum Type { Ingredient = 0, Food = 1}

    public Type _type;

    public CookingTool cookingTool;

    // Start is called before the first frame update
    private void Awake()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }


    public override void SlotClick()
    {
        cookingTool.ReturnIngredient(index);
    }

    public void FoodSlotClick()
    {
        Debug.Log("요리 호출");
        if (cookingTool.isCooked)
        {
            Debug.Log("요리 완료");
            if (InventoryManager.Instance.AddFood(cookingTool.FoodInfos))
            {
                cookingTool.RefreshTool();
                changeSlotUI(cookingTool.beforeCook);
            }
            
        }
    }

    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

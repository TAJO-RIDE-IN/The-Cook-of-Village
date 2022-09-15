using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class CookItemSlot : ItemSlot
{
    public enum Type { Ingredient = 0, Food = 1}

    public Type _type;

    public CookItemSlotManager itemSlotManager;

    public int ingridientId;
    // Start is called before the first frame update
    private void Awake()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }


    public override void SlotClick()
    {
        itemSlotManager.cookingTool.ReturnIngredient(index);
    }

    public void FoodSlotClick()
    {
        
        //Debug.Log("요리 호출");
        if (itemSlotManager.cookingTool.isCooked)
        {
            //Debug.Log("요리 완료");
            if (ChefInventory.Instance.AddFood(itemSlotManager.cookingTool.FoodInfos))
            {
                //itemSlotManager.cookingTool.toolBeforeCook
                //Debug.Log("요리 추가 완료");
                changeSlotUI(itemSlotManager.cookingTool.toolBeforeCook);
                itemSlotManager.cookingTool.RemoveFood();
            }
            return;
        }
        itemSlotManager.cookingTool.Cook();
    }
    


    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

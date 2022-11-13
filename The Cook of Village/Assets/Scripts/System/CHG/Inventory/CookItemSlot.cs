using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItemSlot : ItemSlot
{
    public enum Type { Ingredient = 0, Food = 1}

    public Type _type;

    public CookItemSlotManager itemSlotManager;

    public bool isUsed = false;
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

    public void ReturnTrash()
    {
        if (ChefInventory.Instance._cookingCharacter.trash.trashEdibleItems[index]._itemType ==
            ChefInventory.EdibleItem.ItemType.Ingredient)
        {
            if (ChefInventory.Instance.AddIngredient(ChefInventory.Instance._cookingCharacter.trash
                .trashEdibleItems[index]._ingredientsInfos))
            {
                isUsed = false;
                ChangeSlotUI(itemSlotManager.emptySlot);
            }
            else
            {
                ChefInventory.Instance.chefSlotManager.ShowWarning();
            }
        }
        else
        {
            if (ChefInventory.Instance.AddFood(ChefInventory.Instance._cookingCharacter.trash
                .trashEdibleItems[index]._foodInfos))
            {
                isUsed = false;
                ChangeSlotUI(itemSlotManager.emptySlot);
            }
            else
            {
                ChefInventory.Instance.chefSlotManager.ShowWarning();
            }
        }
        
    }

    public void ThrowTrash()
    {
        itemSlotManager.RefreshSlot();
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
                ChangeSlotUI(itemSlotManager.cookingTool.toolBeforeCook);
                itemSlotManager.cookingTool.RemoveFood();
            }
            return;
        }
        itemSlotManager.cookingTool.Cook();
    }
    


    public void ChangeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

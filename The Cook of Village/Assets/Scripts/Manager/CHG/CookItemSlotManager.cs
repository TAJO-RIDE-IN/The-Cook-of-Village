using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItemSlotManager : ItemSlotManager
{
    public CookItemSlot[] itemslots;
    public CookingTool cookingTool;
    

    private void Awake()
    {
        for (int i = 0; i < ChildSlotCount; i++)//이 작업을 나중에 함수 만들어서 게임 시작할 때 한번에 호출해주자
        {
            itemslots[i].Index = i;
        }
    }

    public void RefreshSlot()
    {
        for (int i = 0; i < ChildSlotCount; i++)
        {
            itemslots[i].changeSlotUI(emptySlot);
        }
    }

    public void ThrowTrash()
    {
        if (ChefInventory.Instance._cookingCharacter)
        {
            cookingTool.ingredientList.Clear();
            RefreshSlot();
        }
    }

    public override void AddIngredientItem(IngredientsInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }
    public override void AddFoodItem(FoodInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }
}

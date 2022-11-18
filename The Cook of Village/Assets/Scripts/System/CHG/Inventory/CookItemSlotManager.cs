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
            //itemslots[i].itemSlotManager = this;
        }
    }

    public void RefreshSlot()
    {
        for (int i = 0; i < ChildSlotCount; i++)
        {
            itemslots[i].ChangeSlotUI(emptySlot);
            itemslots[i].isUsed = false;
        }
    }


    public override void AddIngredientItem(ItemInfos infos, int index)
    {
        itemslots[index].ChangeSlotUI(ImageData.Instance.FindImageData(infos.ImageID));
    }
    public override void AddFoodItem(FoodInfos infos, int index)
    {
        itemslots[index].ChangeSlotUI(ImageData.Instance.FindImageData(infos.ImageID));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItemSlotManager : ItemSlotManager
{
    // Start is called before the first frame update
    public ItemSlot[] itemslots;

    private void Awake()
    {
        for (int i = 0; i < ChildSlotCount; i++)//이 작업을 나중에 함수 만들어서 게임 시작할 때 한번에 호출해주자
        {
            itemslots[i].Index = i;
        }
    }

    public override void AddIngredientItem(ItemInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }
    public override void AddFoodItem(FoodInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }
}

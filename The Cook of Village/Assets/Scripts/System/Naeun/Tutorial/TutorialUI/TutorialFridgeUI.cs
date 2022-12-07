using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFridgeUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        ItemData.Instance.ItemType[(int)ItemType.Type.Fruit].ItemInfos[0].Amount = 10;
        ClickBlock.SetActive(true);
        Controller.NextDialogue();
        Controller.PlayerControl(false, "Fridge");
        EventButton[1] = RestaurantController.IngredientBox;
        RestaurantController.ObjectCollider[3].enabled = false;
        EventButton[2] = null;
        foreach (var button in UIButton)
        {
            button.interactable = false;
        }
    }
    protected override void AddEvent(int index)
    {
        switch (index)
        {
            case 0:
                ClickBlock.SetActive(false);
                Controller.NextDialogue();
                break;
            case 1:
                ClickBlock.SetActive(true);
                Controller.NextDialogue();
                Controller.PlayerControl(true, "Fridge");
                EventButton[index].onClick.Invoke();
                Controller.PlayerControl(false, "Fridge");
                EventButton[index].interactable = true;
                EventButton[index-1].interactable = false;
                EventButton[0].interactable = true;
                EventButton[2] = EventButton[0];
                EventButton[2].onClick.AddListener(NextEvent);
                break;
        }
    }
    protected override void EndEvent()
    {
        Controller.EndEvent();
        RestaurantController.PlayerCook.isSpace = false;
    }
}

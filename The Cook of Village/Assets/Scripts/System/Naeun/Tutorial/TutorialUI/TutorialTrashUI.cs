using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrashUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        EventButton[0] = RestaurantController.IngredientBox;
        //쓰레기UI가 인벤토리보다 뒤에 있기 때문에 다시 선언함.
        ClickImage[0] = RestaurantController.tutorialRestaurantUI.ClickImage.gameObject; 
        Controller.NextDialogue();
        if (RestaurantController.RestaurantDestination[2].activeSelf)
        {
            Controller.NextDialogue();
        }
    }
    protected override void AddEvent(int index)
    {
        if(index.Equals(0))
        {
            EventButton[0].interactable = true;
        }
    }
    protected override void EndEvent()
    {
        Controller.EndEvent();
    }
}

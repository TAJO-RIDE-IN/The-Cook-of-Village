using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestOrder : FoodOrder
{
    public TutorialNPCController tutorialNPCController;
    protected override void Order(FoodInfos infos) //음식주문
    {
        OrderInit(infos);
        tutorialNPCController.tutorialController.NextDialogue();
    }
}

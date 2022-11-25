using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestOrder : FoodOrder
{
    public TutorialNPCController tutorialNPCController;
    protected override void Order(FoodInfos infos) //�����ֹ�
    {
        OrderInit(infos);
        tutorialNPCController.tutorialController.NextDialogue();
    }
}

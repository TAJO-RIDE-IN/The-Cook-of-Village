using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantUI : TutorialUI
{
    public TutorialRestaurantController TutorialController;

    protected override void Disable()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Control"))
        {
            CallDialogue("Purchase");
        }
    }
    protected override void Action()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Restaurant"))
        {
            TutorialController.RestaurantAction();
        }
    }
}

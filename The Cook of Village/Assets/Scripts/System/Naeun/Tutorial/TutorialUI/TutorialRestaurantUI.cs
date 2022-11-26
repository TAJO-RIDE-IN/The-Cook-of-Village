using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantUI : TutorialUI
{
    public TutorialRestaurantController tutorialController;

    protected override void Disable()
    {
        if(dialogueManager.CurrentSentencesName.Equals("Restaurant"))
        {
            tutorialController.EndEvent();
        }
        //CallDialogue(NextDialogue[dialogueManager.CurrentSentencesName]);
    }
    protected override void Action(bool state)
    {
        tutorialController.PlayAction(state);
    }
}

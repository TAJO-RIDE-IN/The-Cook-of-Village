using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantUI : TutorialUI
{
    public TutorialRestaurantController tutorialController;

    protected override void Disable()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Control"))
        {
            CallDialogue("Purchase");
        }
    }
}

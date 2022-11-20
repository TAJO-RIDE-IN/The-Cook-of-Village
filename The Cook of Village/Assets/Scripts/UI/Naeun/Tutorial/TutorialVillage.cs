using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillage : TutorialUI
{
    public TutorialVillageController TutorialController;

    protected override void Disable()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Control"))
        {
            CallDialogue("Purchase");
        }
    }

    protected override void Action()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Purchase"))
        {
            TutorialController.PurchaseAction();
        }
    }
}

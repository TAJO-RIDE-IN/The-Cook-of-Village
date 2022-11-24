using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillageUI : TutorialUI
{
    public TutorialVillageController TutorialController;
    public GameObject Key;

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
            Key.SetActive(false);
        }
    }
}

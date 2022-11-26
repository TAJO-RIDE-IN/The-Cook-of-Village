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

    protected override void Action(bool state)
    {
        if (dialogueManager.CurrentSentencesName.Equals("Purchase"))
        {
            TutorialController.PlayAction(state);
            Key.SetActive(false);
        }
    }
}

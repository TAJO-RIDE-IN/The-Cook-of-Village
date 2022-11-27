using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRestaurantUI : TutorialUI
{
    public TutorialRestaurantController tutorialController;
    public Image ClickImage;

    protected override void Disable()
    {
        if(dialogueManager.CurrentSentencesName.Equals("Restaurant"))
        {
            tutorialController.EndEvent();
        }
        else if(dialogueManager.CurrentSentencesName.Equals("EndTutorial"))
        {
            tutorialController.EndTutorial();
        }
        //CallDialogue(NextDialogue[dialogueManager.CurrentSentencesName]);
    }
    protected override void Action(bool state)
    {
        tutorialController.PlayAction(state);
    }
}

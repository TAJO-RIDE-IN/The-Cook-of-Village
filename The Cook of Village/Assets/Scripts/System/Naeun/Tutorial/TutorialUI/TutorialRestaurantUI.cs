using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantUI : TutorialUI
{
    public TutorialRestaurantController TutorialController;
    protected Dictionary<string, string> NextDialogue = new Dictionary<string, string>()
    {
        {"Control", "Purchase"}, {"Purchase", "Restaurant"}, {"Restaurant", "InstallTool"},
        {"InstallTool", "CookingFood" }, {"CookingFood", "Serving"}
    };
    protected override void Disable()
    {
        //CallDialogue(NextDialogue[dialogueManager.CurrentSentencesName]);
    }
    protected override void Action()
    {
        if (dialogueManager.CurrentSentencesName.Equals("Restaurant"))
        {
            TutorialController.RestaurantAction();
        }
    }
}

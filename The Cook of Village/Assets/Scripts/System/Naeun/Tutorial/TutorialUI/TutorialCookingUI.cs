using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCookingUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        Controller.PlayerControl(false, "Tool");
    }
    protected override void EndEvent()
    {
        Controller.NextDialogue();
    }
}

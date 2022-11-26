using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCookUI : TutorialDetailsUI
{
    protected override void EndEvent()
    {
        Controller.NextDialogue();
    }
}

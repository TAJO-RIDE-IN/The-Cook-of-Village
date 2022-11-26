using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInventoryUI : TutorialDetailsUI
{
    protected override void EndEvent()
    {
        Controller.NextDialogue();
    }
}

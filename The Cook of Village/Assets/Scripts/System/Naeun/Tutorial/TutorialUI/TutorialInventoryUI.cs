using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInventoryUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        Controller.NextDialogue();
    }
    protected override void EndEvent()
    {
        Controller.NextDialogue();
    }
    private void OnDisable()
    {
        EndEvent();
    }
}

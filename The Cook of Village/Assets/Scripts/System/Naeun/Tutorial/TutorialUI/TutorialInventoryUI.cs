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
        Controller.EndEvent();
    }
    private void OnDisable()
    {
        RestaurantController.ToolEnable();
        EndEvent();
    }
}

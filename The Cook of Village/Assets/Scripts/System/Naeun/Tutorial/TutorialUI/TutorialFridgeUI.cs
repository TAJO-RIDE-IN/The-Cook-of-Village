using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFridgeUI : TutorialDetailsUI
{
    protected override void AddEvent(int index)
    {
        switch (index)
        {
            case 0:
                ClickBlock.SetActive(false);
                Controller.NextDialogue();
                break;
            case 1:
                ClickBlock.SetActive(true);
                Controller.NextDialogue();
                EventButton[index].interactable = true;
                break;
        }
    }
    protected override void EndEvent()
    {
        Controller.EndEvent();
    }
}

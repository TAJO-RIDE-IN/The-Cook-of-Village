using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFridgeUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        Controller.NextDialogue();
        Controller.PlayerControl(false, "Fridge");
    }
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
                Controller.PlayerControl(true, "Fridge");
                EventButton[index].onClick.Invoke();
                Controller.PlayerControl(false, "Fridge");
                EventButton[index].interactable = true;
                EventButton[index-1].interactable = false;
                break;
        }
    }
    protected override void EndEvent()
    {
        Controller.EndEvent();
    }
}

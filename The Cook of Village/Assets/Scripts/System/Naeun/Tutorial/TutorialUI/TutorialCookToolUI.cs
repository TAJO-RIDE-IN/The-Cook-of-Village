using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCookToolUI : TutorialDetailsUI
{
    protected override void AddInit()
    {
        Controller.PlayerControl(false, "CookPosition");
    }
    protected override void EndEvent()
    {

    }
}

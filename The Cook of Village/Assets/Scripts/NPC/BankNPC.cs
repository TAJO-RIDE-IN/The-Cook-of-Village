using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankNPC : VillageNPC, IObserver<GameData>
{
    public BankUI bankUI;
    public override void EnterShop()
    {

    }
    public override void UIState(bool state)
    {
        bankUI.UIState(state);
    }
}

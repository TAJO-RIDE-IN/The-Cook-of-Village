using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : VillageNPC
{
    public ShopUI shopUI;
    public override void EnterShop()
    {
        shopUI.shopNPC = this;
    }
    public override void UIState(bool state)
    {
        shopUI.UIState(state);
    }
}

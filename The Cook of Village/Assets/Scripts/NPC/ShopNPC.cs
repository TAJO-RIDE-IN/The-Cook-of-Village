using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : VillageNPC
{
    public ShopUI shopUI;
    public ItemType.Type type;
    public override void EnterShop()
    {
        shopUI.shop = type;
        shopUI.shopNPC = this;
    }
    public override void UIState(bool state)
    {
        shopUI.UIState(state);
    }
}

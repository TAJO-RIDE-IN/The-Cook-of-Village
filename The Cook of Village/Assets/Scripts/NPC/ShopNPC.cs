using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : VillageNPC
{
    public ShopUI shopUI;
    public override void EnterShop()
    {
        shopUI.shop = NPCData.Instance.WorkDataType[npcInfos.work];
        shopUI.shopNPC = this;
    }
    public override void UIState(bool state)
    {
        shopUI.UIState(state);
    }
}

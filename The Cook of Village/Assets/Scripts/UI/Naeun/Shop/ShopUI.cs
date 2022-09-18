using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIController
{
    public GameObject SlotContent;
    public SlotShop[] slot;
    [SerializeField] public ItemType.Type shop;
    public ShopNPC shopNPC;
    public ShopSelect shopSelect;

    public Scrollbar Scroll;

    public void UIState(bool state)
    {
        this.gameObject.SetActive(state);
        Scroll.value = 0;
        if (state) { LoadSlotData(); }
    }
    private List<ItemInfos> ShopInfos()
    {
        List<ItemInfos> infos = new List<ItemInfos>();
        if((int)shop == 6 || (int)shop == 7)
        {
            infos.AddRange(ItemData.Instance.ItemType[6].ItemInfos);
            infos.AddRange(ItemData.Instance.ItemType[7].ItemInfos);
            return infos;
        }
        infos = ItemData.Instance.ItemType[(int)shop].ItemInfos;
        return infos;
    }

    private bool LoadState(ItemInfos info)
    {
        if (shop == ItemType.Type.Potion )
        {
            if(GameData.Instance.Today == 5)
            {
                return true;
            }
            else
            {
                if(info.ID == 54) { return false; }
                return true;
            }
        }
        if(shop == ItemType.Type.Other && info.ID == 40) { return false; }
        return true;
    }

    public void LoadSlotData()
    {
        shopSelect.NPC = shopNPC;
        foreach (var info in ShopInfos().Select((value, index) => (value, index)))
        {
            if(LoadState(info.value))
            {
                slot[info.index].Infos = info.value;
                slot[info.index].gameObject.SetActive(true);
            }
        }
    }
}

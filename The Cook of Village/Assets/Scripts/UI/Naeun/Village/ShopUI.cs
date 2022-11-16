using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopImageContainer
{
    [SerializeField] public NPCInfos.Work shop;
    public Sprite BackGoundImage;
    public Sprite SlotContainerImage;
    public Sprite SlotImage;
    public Sprite SelectSlotBackGoundImage;
    public Sprite BuyButtonImage;
    public Sprite ExitButtonImage;
}
[System.Serializable]
public class ShopUIContainer
{
    public Image BackGoundImage;
    public Image SlotContainerImage;
    public Image SelectSlotBackGoundImage;
    public Image BuyButtonImage;
    public Image ExitButtonImage;
}
public class ShopUI : UIController
{
    [SerializeField] public ShopImageContainer[] ImageContainer;
    [SerializeField] public ShopUIContainer UIContainer;
    public SlotShop[] slot;
    [SerializeField] private ItemType.Type CurrentShop;
    public enum ShopType {Buy, ReSell}
    public ShopType type = ShopType.Buy;
    public GameObject SlotContent;
    public ShopNPC shopNPC;
    public ShopSelect shopSelect;
    public Text ShopName;
    public Dictionary<ItemType.Type, string> DicShopName = new Dictionary<ItemType.Type, string>()
    {
        { ItemType.Type.Fruit, "과일" }, {ItemType.Type.Vegetable, "야채"}, {ItemType.Type.Meat, "고기"},
        { ItemType.Type.Other, "초콜렛" }, { ItemType.Type.Potion, "포션" }, { ItemType.Type.CookingTool, "인테리어" }
    };
    public Dictionary<ItemType.Type, int> DicShopImageIndex = new Dictionary<ItemType.Type, int>()
    {
        { ItemType.Type.Fruit, 0 }, {ItemType.Type.Vegetable, 0}, {ItemType.Type.Meat, 0},
        { ItemType.Type.Other, 0}, { ItemType.Type.Potion, 1 }, { ItemType.Type.CookingTool, 2}
    };

    private ItemData itemData;
    public void UIState(bool state)
    {
        itemData = ItemData.Instance;
        CurrentShop = NPCData.WorkDataType[shopNPC.npcInfos.work];
        this.gameObject.SetActive(state);
        if (state) 
        {
            LoadSlotData();
            ChangeSelectSlotData();
        }
    }
    public void ResellToggle(Toggle toggle)
    {
        type = (toggle.isOn) ? ShopType.ReSell : ShopType.Buy;
        LoadSlotData();
        ChangeSelectSlotData();
    }
    private List<ItemInfos> ShopInfos()
    {
        List<ItemInfos> infos = new List<ItemInfos>();
        if((int)CurrentShop == 6 || (int)CurrentShop == 7)
        {
            infos.AddRange(itemData.ItemType[6].ItemInfos);
            infos.AddRange(itemData.ItemType[7].ItemInfos);
            infos.Remove(itemData.ItemType[6].ItemInfos[3]); //접시는 판매하지 않음
            return infos;
        }
        infos = itemData.ItemType[(int)CurrentShop].ItemInfos;
        return infos;
    }

    private bool LoadState(ItemInfos info)
    {
        if (CurrentShop == ItemType.Type.Potion )
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
        if(CurrentShop == ItemType.Type.Other && info.ID == 40) { return false; }
        return true;
    }
    public void Init()
    {
        foreach (var _slot in slot)
        {
            _slot.gameObject.SetActive(false);
        }
    }
    public void LoadSlotData()
    {
        Init();
        ChangeShopUI();
        shopSelect.NPC = shopNPC;
        foreach (var info in ShopInfos().Select((value, index) => (value, index)))
        {
            if(LoadState(info.value))
            {
                slot[info.index].shopUI = this;
                slot[info.index].gameObject.SetActive(true);               
                slot[info.index].ModifyPrice = ModifyPrice(info.value.Price);
                slot[info.index].itemInfos = info.value;
            }
        }
    }
    private int ModifyPrice(int price)
    {
        int shopPrice = NPCData.Instance.NPCShopPrice(shopNPC.npcInfos.work, price);
        if (type == ShopType.ReSell)
        {
            shopPrice = (int)Math.Round(shopPrice * 0.5f);
            return shopPrice;
        }
        return shopPrice;
    }
    private void ChangeUI(int index)
    {
        UIContainer.BackGoundImage.sprite = ImageContainer[index].BackGoundImage;
        UIContainer.SlotContainerImage.sprite = ImageContainer[index].SlotContainerImage;
        UIContainer.SelectSlotBackGoundImage.sprite = ImageContainer[index].SelectSlotBackGoundImage;
        UIContainer.BuyButtonImage.sprite = ImageContainer[index].BuyButtonImage;
        UIContainer.ExitButtonImage.sprite = ImageContainer[index].ExitButtonImage;

        foreach(var image in slot)
        {
            image.SlotBackground.sprite = ImageContainer[index].SlotImage;
        }
    }
    private void ChangeShopUI()
    {
        string name = "상점";
        ShopName.text = DicShopName[CurrentShop] + " " + name;
        //ChangeUI(DicShopImageIndex[CurrentShop]);
    }
    private void ChangeSelectSlotData()
    {
        foreach (var _slot in slot)
        {
            if(_slot.gameObject.activeSelf == true)
            {
                shopSelect.Infos = _slot.Infos;
                break;
            }
        }
    }
}

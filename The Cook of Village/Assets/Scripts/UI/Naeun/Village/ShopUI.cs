using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIController
{
    public SlotShop[] slot;
    [SerializeField] private ItemType.Type CurrentShop;
    [HideInInspector] public NPCInfos.Work npcWork;
    [SerializeField] public enum ShopType { Buy, ReSell }
    private ShopType type;
    public ShopType Type
    {
        get { return type; }
        set
        {
            type = value;
            ServiceButton.sprite = (value == ShopType.Buy) ? BuyButtonImage : ResellButtonImage;
            Color color = (value == ShopType.Buy) ? BuyColor : ResellColor;
            BackgroundImage.color = color;

        }
    }
    public ShopNPC shopNPC;
    public ShopSelect shopSelect;
    public Text ShopName;
    public Toggle ResellToggle;
    public Scrollbar ShopScrollbar;
    public Image BackgroundImage;
    public Image ServiceButton;
    public Sprite ResellButtonImage;
    public Sprite BuyButtonImage;
    public Color ResellColor;
    public Color BuyColor;
    public Dictionary<ItemType.Type, string> DicShopName = new Dictionary<ItemType.Type, string>()
    {
        { ItemType.Type.Fruit, "과일" }, {ItemType.Type.Vegetable, "야채"}, {ItemType.Type.Meat, "고기"},
        { ItemType.Type.Other, "초콜렛" }, { ItemType.Type.Potion, "포션" }, { ItemType.Type.CookingTool, "인테리어" }
    };

    private ItemData itemData;
    public void UIState(bool state)
    {
        itemData = ItemData.Instance;
        CurrentShop = NPCData.WorkDataType[shopNPC.npcInfos.work];
        npcWork = shopNPC.npcInfos.work;
        ResellToggle.isOn = false;
        Type = ShopType.Buy;
        this.gameObject.SetActive(state);
        if (state)
        {
            if (ShopScrollbar != null)
            {
                ShopScrollbar.value = 1;
            }
            LoadSlotData();
            ChangeSelectSlotData();
        }
    }
    public void ResellToggleClick(Toggle toggle)
    {
        Type = (toggle.isOn) ? ShopType.ReSell : ShopType.Buy;
        LoadSlotData();
        ChangeSelectSlotData();
    }
    private List<ItemInfos> ShopInfos()
    {
        List<ItemInfos> infos = new List<ItemInfos>();
        if (npcWork.Equals(NPCInfos.Work.interiorShop))
        {
            infos.AddRange(itemData.ItemType[6].ItemInfos);
            infos.AddRange(itemData.ItemType[7].ItemInfos);
            infos.Remove(itemData.ItemType[6].ItemInfos[3]); //접시는 판매하지 않음
            return infos;
        }
        infos = itemData.ItemType[(int)CurrentShop].ItemInfos;
        return infos;
    }

    private bool LoadState(ItemInfos info) //추가하거나 제거해야하는 아이템인 경우 판단
    {
        if (CurrentShop.Equals(ItemType.Type.Potion))
        {
            if (GameData.Instance.Today.Equals(5))
            {
                return true;
            }
            else
            {
                if (info.ID.Equals(54)) { return false; }//금요일에만 무지개 포션 판매
                return true;
            }
        }
        if (CurrentShop.Equals(ItemType.Type.Other) && info.ID.Equals(40)) { return false; }
        return true;
    }
    public void Init()
    {
        foreach (var _slot in slot)
        {
            if (_slot != null)
            {
                _slot.gameObject.SetActive(false);
            }
        }
    }
    public void LoadSlotData()
    {
        Init();
        ChangeShopUI();
        shopSelect.NPC = shopNPC;
        foreach (var info in ShopInfos().Select((value, index) => (value, index)))
        {
            if (LoadState(info.value))
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
        if (Type.Equals(ShopType.ReSell))
        {
            shopPrice = (int)Math.Round(shopPrice * 0.5f);
            return shopPrice;
        }
        return shopPrice;
    }
    private void ChangeShopUI()
    {
        string name = "상점";
        ShopName.text = DicShopName[CurrentShop] + " " + name;
    }
    private void ChangeSelectSlotData()
    {
        foreach (var _slot in slot)
        {
            if(_slot != null)
            {
                if (_slot.gameObject.activeSelf)
                {
                    shopSelect.ModifyPrice = ModifyPrice(_slot.Infos.Price);
                    shopSelect.Infos = _slot.Infos;
                    break;
                }
            }
        }
    }
}

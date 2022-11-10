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
    [SerializeField] public ItemType.Type CurrentShop;
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
    public void UIState(bool state)
    {
        this.gameObject.SetActive(state);
        if (state) 
        {
            LoadSlotData();
            ChangeSelectSlotData();
        }
    }
    private List<ItemInfos> ShopInfos()
    {
        List<ItemInfos> infos = new List<ItemInfos>();
        if((int)CurrentShop == 6 || (int)CurrentShop == 7)
        {
            infos.AddRange(ItemData.Instance.ItemType[6].ItemInfos);
            infos.AddRange(ItemData.Instance.ItemType[7].ItemInfos);
            infos.Remove(ItemData.Instance.ItemType[6].ItemInfos[5]);
            return infos;
        }
        infos = ItemData.Instance.ItemType[(int)CurrentShop].ItemInfos;
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
                slot[info.index].gameObject.SetActive(true);
                slot[info.index].itemInfos = info.value;
            }
        }
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
        ChangeUI(DicShopImageIndex[CurrentShop]);
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

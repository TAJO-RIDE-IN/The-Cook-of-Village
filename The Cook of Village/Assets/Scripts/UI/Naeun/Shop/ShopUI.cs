using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopUI : SlotParent
{
    public GameObject SlotContent;
    public SlotShop[] slot;
    public enum Shop {Fruit, Vegetable, Meat, Potion, CookingTool, Funiture}
    [SerializeField]
    public Shop shop;

    private void Awake()
    {
        slot = SlotContent.transform.GetComponentsInChildren<SlotShop>(true);
    }
    public override void OpenUI()
    {
        GameManager.Instance.IsUI = true;
        this.gameObject.SetActive(true);
        LoadSlotData();
    }
    public override void CloseUI()
    {
        GameManager.Instance.IsUI = false;
        this.gameObject.SetActive(false);
    }

    public override void LoadSlotData ()
    {
        List <ItemInfos> infos = ItemData.Instance.ItemType[(int)shop+1].ItemInfos;

        foreach (var Ingredient in infos.Select((value, index) => (value, index)))
        {
            slot[Ingredient.index].Infos = Ingredient.value;
            slot[Ingredient.index].gameObject.SetActive(true);
        }
    }
}

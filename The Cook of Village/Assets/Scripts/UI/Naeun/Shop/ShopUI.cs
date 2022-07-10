using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : SlotParent
{
    public GameObject SlotContent;
    public SlotShop[] slot;
    public enum Shop {Fruit, Vegetable, Meat, Potion}
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

    private void OnEnable()
    {
        LoadSlotData();
    }

    public override void LoadSlotData()
    {
        int order = 0;
        int type = SlotDictionary[shop.ToString()];

        foreach(IngredientsInfos materialInfos in IngredientsData.Instance.IngredientsType[type].IngredientsInfos)
        {
            slot[order].ingredientsInfos = materialInfos;
            slot[order].gameObject.SetActive(true);
            order++;
        }
    }
}

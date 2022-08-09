using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject SlotContent;
    public SlotShop[] slot;
    [SerializeField]
    public ItemType.Type shop;

    public void OpenUI()
    {
        GameManager.Instance.IsUI = true;
        this.gameObject.SetActive(true);
        LoadSlotData();
    }
    public void CloseUI()
    {
        GameManager.Instance.IsUI = false;
        this.gameObject.SetActive(false);
    }

    public void LoadSlotData ()
    {
        List <ItemInfos> infos = ItemData.Instance.ItemType[(int)shop].ItemInfos;

        foreach (var Ingredient in infos.Select((value, index) => (value, index)))
        {
            slot[Ingredient.index].Infos = Ingredient.value;
            slot[Ingredient.index].gameObject.SetActive(true);
        }
    }
}

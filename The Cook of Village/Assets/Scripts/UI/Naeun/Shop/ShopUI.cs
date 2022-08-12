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

    private bool PotionState(ItemInfos info)
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
        return true;
    }

    public void LoadSlotData ()
    {
        List <ItemInfos> infos = ItemData.Instance.ItemType[(int)shop].ItemInfos;

        foreach (var info in infos.Select((value, index) => (value, index)))
        {
            if(PotionState(info.value))
            {
                slot[info.index].Infos = info.value;
                slot[info.index].gameObject.SetActive(true);
            }
        }
    }
}

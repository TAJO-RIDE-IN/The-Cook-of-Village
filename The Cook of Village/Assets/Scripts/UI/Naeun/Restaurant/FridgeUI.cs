using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class FirdgeSlotContainer
{
    public ItemType.Type ingredientsType;
    public List<SlotFridge> FridgeSlot = new List<SlotFridge>();
}
public class FridgeUI : UIController
{
    [SerializeField]
    private FirdgeSlotContainer[] SlotContainer;

    public void LoadSlotData()
    {
        foreach(var Container in SlotContainer)
        {
            List<ItemInfos> ingredients = ItemData.Instance.ItemType[(int)Container.ingredientsType].ItemInfos;
            foreach (var Infos in ingredients.Select((value, index) => (value, index)))
            {
                Container.FridgeSlot[Infos.index].Infos = Infos.value;
                Container.FridgeSlot[Infos.index].SlotCount = Infos.value.Amount;
                Container.FridgeSlot[Infos.index].FridgeUI = this;
            }
        }
    }

    public void InputRefrigerator(int id, int amount)
    {
        ItemData.Instance.ChangeAmount(id, ItemData.Instance.ItemInfos(id).Amount+amount);
        LoadSlotData();
    }

    public void FridgeUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (this.gameObject.activeSelf)
        {
            LoadSlotData();
        }
        else
        {
            GameObject fridge = GameObject.FindGameObjectWithTag("Fridge");
            if (fridge != null)
            {
                fridge.GetComponent<Fridge>().FridgeAnimaion(false);
            }
        }
    }
}

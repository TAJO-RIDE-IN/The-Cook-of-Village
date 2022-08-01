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
public class FridgeUI : SlotParent
{
    [SerializeField]
    private FirdgeSlotContainer[] SlotContainer;

    public override void LoadSlotData()
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

    public void InputRefrigerator(int type, int id, int amount)
    {
        ItemData.Instance.ChangeAmount(type, id, amount++);
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
        GameObject fridge = GameObject.FindGameObjectWithTag("Fridge");
        if(fridge != null)
        {
            fridge.GetComponent<Fridge>().FridgeAnimaion(false);
        }
    }
}

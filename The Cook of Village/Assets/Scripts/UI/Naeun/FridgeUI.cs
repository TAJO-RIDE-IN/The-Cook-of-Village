using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class FirdgeSlotContainer
{
    public IngredientsType.Type ingredientsType;
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
            List<IngredientsInfos> ingredients = IngredientsData.Instance.IngredientsType[(int)Container.ingredientsType].IngredientsInfos;
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
        IngredientsData.Instance.ChangeAmount(type, id, amount++);
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

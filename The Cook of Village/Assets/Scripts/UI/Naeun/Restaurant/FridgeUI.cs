using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FridgeUI : UIController
{
    [SerializeField] private SlotFridge[] slotFridge;
    [HideInInspector]public Fridge fridge;
    public List<ItemInfos> ingredients = new List<ItemInfos>();
    private ItemData itemdata;

    protected override void Enable()
    {
        itemdata = ItemData.Instance;
        ingredients = itemdata.IngredientList();
    }
    protected override void Disable()
    {
        FridgeUIState(false);
    }
    public void LoadSlotData()
    {
        foreach (var Infos in ingredients.Select((value, index) => (value, index)))
        {
            slotFridge[Infos.index].itemInfos = Infos.value;
        }
    }

    public void InputRefrigerator(int id, int amount)
    {
        itemdata.ChangeAmount(id, amount);
        foreach (var Infos in ingredients.Select((value, index) => (value, index)))
        {
            if (Infos.value.ID == id)
            {
                slotFridge[Infos.index].ModifySlot();
            }
        }
    }

    public void FridgeUIState(bool state)
    {
        this.gameObject.SetActive(state);
        if (state)
        {
            LoadSlotData();
        }
        else
        {
            if (fridge != null)
            {
                fridge.ChangeState(false);
            }
        }
    }
}

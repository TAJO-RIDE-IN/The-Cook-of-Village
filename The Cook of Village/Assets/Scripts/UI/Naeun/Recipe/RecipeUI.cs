using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeUI : UIController
{
    public List<SlotRecipe> FoodSlot = new List<SlotRecipe>();
    [SerializeField]
    private FoodTool.Type type;
    public FoodTool.Type CurrentTool
    {
        get { return type; }
        set
        {
            type = value;
            LoadRecipeSlot();
        }
    }
    public ToggleControl toggleControl;

    public void RecipeUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (this.gameObject.activeSelf)
        {
            CurrentTool = FoodTool.Type.Blender;
            LoadRecipeSlot();
        }
        toggleControl.ResetToggle(0);
    }

    public void ResetSlot()
    {
        foreach(SlotRecipe slot in FoodSlot)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void ClickToolToggle(int type)
    {
        CurrentTool = (FoodTool.Type)type;
    }

    public void LoadRecipeSlot()
    {
        ResetSlot();
        List<FoodInfos> infos = FoodData.Instance.foodTool[(int)CurrentTool].foodInfos;
        foreach (var food in infos.Select((value, index) => (value, index)))
        {
            FoodSlot[food.index].foodInfos = food.value;
        }
        FoodSlot[0].SelectSlot();
    }
}

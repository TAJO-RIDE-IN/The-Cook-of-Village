using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    public List<RecipeSlot> FoodSlot = new List<RecipeSlot>();

    private void Start()
    {
        LoadRecipeSlot(0); //첫 페이지 믹서기
    }
    public void Init()
    {
        foreach(RecipeSlot slot in FoodSlot)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void LoadRecipeSlot(int ToolID)
    {
        Init();
        List<FoodInfos> infos = FoodData.Instance.foodTool[ToolID].foodInfos;
        foreach (var food in infos.Select((value, index) => (value, index)))
        {
            FoodSlot[food.index].foodInfos = food.value;
        }
    }

    public void RecipeButton()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        GameManager.Instance.IsUI = this.gameObject.activeSelf;
    }
}

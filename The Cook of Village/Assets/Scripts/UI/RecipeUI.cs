using System.Collections.Generic;
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
        int order = 0;
        foreach (FoodInfos food in FoodData.Instance.foodTool[ToolID].foodInfos)
        {
            FoodSlot[order].gameObject.SetActive(true);
            FoodSlot[order].foodInfos = food;
            FoodSlot[order].ChangeImage();
            order++;
        }
    }

    public void RecipeExitButton()
    {
        GameManager.Instance.IsUI = false;
        this.gameObject.SetActive(false);
    }

    public void OpenRecipe()
    {
        GameManager.Instance.IsUI = true;
        this.gameObject.SetActive(true);
    }
}

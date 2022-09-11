using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolButton : MonoBehaviour
{
    public FoodTool.Type type;
    public RecipeUI recipeUI;
    public void ClickToolButton()
    {
        recipeUI.LoadRecipeSlot((int)type);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    public FoodTool.Type type;
    public RecipeUI recipeUI;
    private Toggle toggle;
    private void OnEnable()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        toggle.group.SetAllTogglesOff();
    }
    public void ClickToolButton()
    {
        recipeUI.LoadRecipeSlot((int)type);
    }
}

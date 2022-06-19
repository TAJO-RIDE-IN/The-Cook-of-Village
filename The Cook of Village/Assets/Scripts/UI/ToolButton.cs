using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolButton : MonoBehaviour
{
    public enum Type { Blender, Pot, Frypan, Plate, Failure }
    [SerializeField]
    public Type type;
    public RecipeUI recipeUI;
    public void ClickToolButton()
    {
        recipeUI.LoadRecipeSlot((int)type);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    public FoodInfos foodInfos;
    public Image FoodImage;
    public SelectRecipe selectRecipe;

    public void ChangeImage()
    {
        FoodImage.sprite = foodInfos.ImageUI;
    }

    public void ClickSlot()
    {
        selectRecipe.foodInfos = foodInfos;
        selectRecipe.LoadSlot();
    }
}

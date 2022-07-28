using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    private FoodInfos infos;
    public FoodInfos foodInfos
    {
        get { return infos; }
        set 
        { 
            infos = value;
            this.gameObject.SetActive(true);
            ChangeImage();
        }
    }
    public Image FoodImage;
    public SelectRecipe selectRecipe;

    private void ChangeImage()
    {
        FoodImage.sprite = foodInfos.ImageUI;
    }

    public void ClickSlot()
    {
        selectRecipe.foodInfos = foodInfos;
        selectRecipe.LoadSlot();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRecipe : Slot<FoodInfos>
{
    public FoodInfos foodInfos
    {
        get { return Infos; }
        set 
        {
            Infos = value;
            this.gameObject.SetActive(true);
            ModifySlot();
        }
    }
    public Image FoodImage;
    public SelectRecipe selectRecipe;

    public override void ModifySlot()
    {
        FoodImage.sprite = ImageData.Instance.FindImageData(foodInfos.ImageID);
    }

    public override void SelectSlot()
    {
        selectRecipe.foodInfos = foodInfos;
        selectRecipe.LoadSlot();
    }
}

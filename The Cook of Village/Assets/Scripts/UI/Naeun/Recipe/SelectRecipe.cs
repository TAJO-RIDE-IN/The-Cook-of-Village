using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRecipe : MonoBehaviour
{
    public FoodInfos foodInfos;
    public List<Image> IngredientImage = new List<Image>();
    public Image FoodImage;
    public Text FoodText;
    public Text FoodExplanation;

    public void Init()
    {
        foreach (Image ingredientImage in IngredientImage)
        {
            ingredientImage.gameObject.SetActive(false);
        }
    }

    public void LoadSlot()
    {
        Init();
        int count = 0;
        FoodImage.sprite = foodInfos.ImageUI;
        FoodExplanation.text = Explanation.FoodExplanation[foodInfos.ID];
        FoodText.text = Localization.GetLocalizedString("Food", foodInfos.Name);
        foreach(int ingredientID in foodInfos.Recipe)
        {
            ItemInfos infos = ItemData.Instance.IngredientsInfos(ingredientID);
            IngredientImage[count].gameObject.SetActive(true);
            IngredientImage[count].sprite = infos.ImageUI;
            count++;
        }
    }
}

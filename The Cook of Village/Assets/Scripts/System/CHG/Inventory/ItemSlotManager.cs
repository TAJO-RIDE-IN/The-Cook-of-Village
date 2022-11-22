using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    public int ChildSlotCount;
    public Text WarningText;
    
    public Sprite emptyInven;
    

    public virtual void AddIngredientItem(ItemInfos infos, int index) { }
    public virtual void AddFoodItem(FoodInfos infos, int index) { }

    public void ShowWarning()
    {
        StartCoroutine(TextFadeOut());
    }

    public IEnumerator TextFadeOut()
    {
        WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, 1f);
        while (WarningText.color.a > 0.0f)
        {
            WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, WarningText.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        
    }
}

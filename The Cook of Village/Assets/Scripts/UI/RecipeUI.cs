using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    public void RecipeExitButton()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenRecipe()
    {
        this.gameObject.SetActive(true);
    }


}

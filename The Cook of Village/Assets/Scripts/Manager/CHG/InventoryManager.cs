using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public class EdibleItem
    {
        enum ItemType
        {
            Ingredient, Food
        }

        private IngredientsData _ingredientsData;
        private FoodData _foodData;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public CookItemSlotManager cookSlotManager;
    public ChefInventory.EdibleItem[] trashEdibleItems;
    public GameObject BigInventory;
    
    public bool AddIngredient(ItemInfos ingredient)
    {
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++)
        {
            if (cookSlotManager.itemslots[i].isUsed == false)
            {
                trashEdibleItems[i]._itemType = ChefInventory.EdibleItem.ItemType.Ingredient;
                trashEdibleItems[i]._ingredientsInfos = ingredient;
                trashEdibleItems[i]._foodInfos = null;
                cookSlotManager.AddIngredientItem(ingredient, i);
                cookSlotManager.itemslots[i].isUsed = true;
                cookSlotManager.itemslots[i].ChangeSlotUI(ImageData.Instance.FindImageData(ingredient.ImageID));
                return true;
            }
        }
        cookSlotManager.ShowWarning();
        return false;
        
    }
    public bool AddFood(FoodInfos food)
    {
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++)
        {
            if (cookSlotManager.itemslots[i].isUsed == false)
            {
                //Debug.Log(i+"번째 ?�롯??비어?�음");
                trashEdibleItems[i]._itemType = ChefInventory.EdibleItem.ItemType.Food;
                trashEdibleItems[i]._ingredientsInfos = null;
                trashEdibleItems[i]._foodInfos = food;
                cookSlotManager.AddFoodItem(food, i);
                cookSlotManager.itemslots[i].isUsed = true;
                cookSlotManager.itemslots[i].ChangeSlotUI(ImageData.Instance.FindImageData(food.ImageID));
                return true;
            }
            //SlotManager.AddFoodItem(food);
        }
        cookSlotManager.ShowWarning();
        return false;
    }

    public void CloseUI()
    {
        BigInventory.SetActive(false);
    }
    public bool PutTrash(int id, Sprite sprite) //?�걸 ?�재 ?�고?�는�?null???�닐?�만 ?�행?�켜주면 ?�는???�시몰라???�번 ??조건�??�음
    {
        
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++) //?�단 ?�시?�에 ?�어가??최�? ?�료 개수가 3개라�??�을 ??
        {
            if (!cookSlotManager.itemslots[i].isUsed)
            {
                /*_animation.Play(toolID.ToString());
                        
                ingredientList.Add(id);
                cookSlotManager.itemslots[i].isUsed = true;
                cookSlotManager.itemslots[i].changeSlotUI(sprite);
                cookSlotManager.itemslots[i].ingridientId = id;
                if (toolID != ToolID.Trash)
                {
                    Ing[i].sprite = sprite;
                    IngredientInven.SetActive(true);
                }*/
                return true;
            }
            
        }

        return false;
    }
}

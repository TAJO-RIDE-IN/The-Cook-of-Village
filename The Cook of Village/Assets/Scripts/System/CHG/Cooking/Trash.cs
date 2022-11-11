using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public CookItemSlotManager cookSlotManager;
    
    
    
    public ChefInventory.EdibleItem[] trashEdibleItems;
    
    public bool AddIngredient(ItemInfos ingredient)
    {
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++)
        {
            Debug.Log(i+"번째 슬롯 진입");
            if (cookSlotManager.itemslots[i].isUsed == false)
            {
                Debug.Log(i+"번째 슬롯이 비어있음");
                trashEdibleItems[i]._itemType = ChefInventory.EdibleItem.ItemType.Ingredient;
                trashEdibleItems[i]._ingredientsInfos = ingredient;
                trashEdibleItems[i]._foodInfos = null;
                cookSlotManager.AddIngredientItem(ingredient, i);
                cookSlotManager.itemslots[i].isUsed = true;
                cookSlotManager.itemslots[i].changeSlotUI(ingredient.ImageUI);
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
                //Debug.Log(i+"번째 슬롯이 비어있음");
                trashEdibleItems[i]._itemType = ChefInventory.EdibleItem.ItemType.Food;
                trashEdibleItems[i]._ingredientsInfos = null;
                trashEdibleItems[i]._foodInfos = food;
                cookSlotManager.AddFoodItem(food, i);
                cookSlotManager.itemslots[i].isUsed = true;
                cookSlotManager.itemslots[i].changeSlotUI(food.ImageUI);
                return true;
            }
            //SlotManager.AddFoodItem(food);
        }
        cookSlotManager.ShowWarning();
        return false;
    }
    
    public bool PutTrash(int id, Sprite sprite) //이걸 현재 들고있는게 null이 아닐때만 실행시켜주면 되는데 혹시몰라서 한번 더 조건문 넣음
    {
        
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++) //일단 레시피에 들어가는 최대 재료 개수가 3개라고 했을 때
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

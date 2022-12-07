using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItemSlot : ItemSlot
{
    public enum Type { Ingredient = 0, Food = 1}

    public Type _type;

    public CookItemSlotManager itemSlotManager;

    public bool isUsed = false;
    public int ingridientId;

    private SoundManager _soundManager;

    private ChefInventory _chefInventory;
    // Start is called before the first frame update
    private void Awake()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        _chefInventory = ChefInventory.Instance;
    }


    public override void SlotClick()
    {
        itemSlotManager.cookingTool.ReturnIngredient(index);
    }

    public void ReturnTrash()
    {
        if (isUsed)
        {
            if ( _chefInventory._cookingCharacter.trash.trashEdibleItems[index]._itemType ==
                 ChefInventory.EdibleItem.ItemType.Ingredient)
            {
                if ( _chefInventory.AddIngredient(ChefInventory.Instance._cookingCharacter.trash
                    .trashEdibleItems[index]._ingredientsInfos))
                {
                    isUsed = false;
                    ChangeSlotUI(itemSlotManager.emptyInven);
                }
                else
                {
                    _chefInventory.chefSlotManager.ShowWarning();
                }
            }
            else
            {
                if ( _chefInventory.AddFood(ChefInventory.Instance._cookingCharacter.trash
                    .trashEdibleItems[index]._foodInfos))
                {
                    isUsed = false;
                    ChangeSlotUI(itemSlotManager.emptyInven);
                }
                else
                {
                    _chefInventory.chefSlotManager.ShowWarning();
                }
            }
        }
    }

    public void ThrowTrash()
    {
        itemSlotManager.RefreshSlot();
        _soundManager.Play(_soundManager._audioClips["Garbage Can"]);
    }

    public void FoodSlotClick()
    {
        
        //Debug.Log("요리 호출");
        if (itemSlotManager.cookingTool.isCooked)
        {
            //if(itemSlotManager.cookingTool.FoodInfos.ID)
            //Debug.Log("요리 완료");
            if (itemSlotManager.cookingTool.FoodInfos.ID == 40)
            {
                if ( _chefInventory.AddIngredient(ItemData.Instance.ItemType[4].ItemInfos[0]))
                {
                    //itemSlotManager.cookingTool.toolBeforeCook
                    //Debug.Log("요리 추가 완료");
                    ChangeSlotUI(itemSlotManager.cookingTool.toolBeforeCook);
                    //_soundManager.Play(_soundManager._audioClips["Plate"]);
                    itemSlotManager.cookingTool.RemoveFood();
                }

                return;
            }
            if ( _chefInventory.AddFood(itemSlotManager.cookingTool.FoodInfos))
            {
                //itemSlotManager.cookingTool.toolBeforeCook
                //Debug.Log("요리 추가 완료");
                ChangeSlotUI(itemSlotManager.cookingTool.toolBeforeCook);
                _soundManager.Play(_soundManager._audioClips["Plate"]);
                itemSlotManager.cookingTool.RemoveFood();
            }
            return;
        }
        itemSlotManager.cookingTool.Cook();
    }
    


    public void ChangeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

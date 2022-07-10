using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    public Text WarningText;
    
    public Sprite lockedSlot;
    public Sprite emptySlot;
    
    
    private InventoryManager _inventoryManager;
    public ItemSlot[] itemslots;
    void Start()
    {
        itemslots = transform.GetComponentsInChildren<ItemSlot>();
        _inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredientItem(IngredientsInfos infos)
    {
        for (int i = 0; i < _inventoryManager.MaxInven; i++)
        {
            if (itemslots[i].isBeingUsed == false)
            {
                itemslots[i].isBeingUsed = true;
                itemslots[i].changeSlotUI(infos.ImageUI);
                return;
            }
        }
    }
    public void AddFoodItem(FoodInfos infos)
    {
        for (int i = 0; i < _inventoryManager.MaxInven; i++)
        {
            if (itemslots[i].isBeingUsed == false)
            {
                itemslots[i].isBeingUsed = true;
                itemslots[i].changeSlotUI(infos.ImageUI);
                return;
            }
        }
    }

    public void ShowWarning()
    {
        StartCoroutine(TextFadeOut());
    }

    public IEnumerator TextFadeOut()
    {
        WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, 1f);
        //InvenWarning.SetActive(true);
        int loopNum = 0;
        //InvenWarning.color = Color.
        while (WarningText.color.a > 0.0f)
        {
            WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, WarningText.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        //yield return null;
        if(loopNum++ > 10000)
            throw new System.Exception("Infinite Loop");
    }
}

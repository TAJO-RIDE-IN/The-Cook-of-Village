using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    public Text WarningText;
    
    public Sprite lockedSlot;
    public Sprite emptySlot;
    public int ChildSlotCount;
    
    private InventoryManager _inventoryManager;
    public ItemSlot[] itemslots;
    void Start()
    {
        
        for (int i = 0; i < ChildSlotCount; i++)//이 작업을 나중에 함수 만들어서 게임 시작할 때 한번에 호출해주자
        {
            itemslots[i].Index = i;
        }
        _inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredientItem(IngredientsInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }
    public void AddFoodItem(FoodInfos infos, int index)
    {
        itemslots[index].changeSlotUI(infos.ImageUI);
    }

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

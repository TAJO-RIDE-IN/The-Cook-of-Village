using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFridge : Slot
{
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
    private InventoryManager _inventoryManager;
    private Transform player;
    private CookingCharacter cook;
    public FridgeUI FridgeUI;
    public int SlotCount
    {
        get { return ingredientsInfos.Amount; }
        set
        {
            ingredientsInfos.Amount = value;
            ModifySlot();
            SlotState();
            IngredientsData.Instance.ChangeAmount(ingredientsInfos.Type, ingredientsInfos.ID, ingredientsInfos.Amount);
        }
    }
    private void Start()
    {
        _inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cook = player.transform.GetComponent<CookingCharacter>();
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        bool state;
        state = (ingredientsInfos.Amount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
        IngredientName.text = Localization.GetLocalizedString("Ingredient", ingredientsInfos.Name);
        IngredientImage.sprite = ingredientsInfos.ImageUI;
    }
    public override void SelectSlot()
    {
        if (ingredientsInfos.Amount > 0)
        {
            if (_inventoryManager.AddIngredient(ingredientsInfos))
            {
                SlotCount--;
                
            }
        }
    }
}

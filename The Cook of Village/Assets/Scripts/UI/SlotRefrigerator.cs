using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRefrigerator : Slot
{
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
    private Transform player;
    private CookingCharacter cook;
    public RefrigeratorUI refrigeratorUI;
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
            if (!cook.isHand)
            {
                SlotCount--;
                cook.currentIngredient = ingredientsInfos;
                cook.isHand = true;
                refrigeratorUI.CloseUI();
                Instantiate(ingredientsInfos.PrefabMaterial, cook.HandPosition.transform.position, Quaternion.identity, cook.HandPosition.transform);
            }
            else
            {
                //손에 뭐 들고있는데 재료슬롯 눌렀을 때 행동
            }
        }
    }
}

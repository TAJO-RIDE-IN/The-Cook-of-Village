/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRefrigerator : Slot
{
    public GameObject RefrigeratorUI;
    public Refrigerator refrigerator;
    public Text CountText;
    private Transform player;
    private CookingCharacter cook;
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
    }
    public override void SelectSlot()
    {
        if (ingredientsInfos.Amount > 0)
        {
            if (!cook.isHand)
            {
                SlotCount--;
                refrigerator.CloseUI();
                cook.currentIngredient = ingredientsInfos;
                cook.isHand = true;
                Instantiate(ingredientsInfos.PrefabMaterial, cook.HandPosition.transform.position, Quaternion.identity, cook.HandPosition.transform);
            }
            else
            {
                //손에 뭐 들고있는데 재료슬롯 눌렀을 때 행동
            }
        }
    }
}

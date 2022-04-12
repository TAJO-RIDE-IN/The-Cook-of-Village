/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public GameObject refrigeratorUI;
    public MaterialData data;
    public GameObject FruitContent;
    public GameObject VegetableContent;
    public GameObject MeetContent;
    private List<SlotRefrigerator> FruitSlot = new List<SlotRefrigerator>();
    private List<SlotRefrigerator> VegetableSlot = new List<SlotRefrigerator>();
    private List<SlotRefrigerator> MeetSlot = new List<SlotRefrigerator>();

    Dictionary<int, List<SlotRefrigerator>> SlotDictionary = new Dictionary<int, List<SlotRefrigerator>>();

    private void Awake()
    {
        FruitSlot = FruitContent.transform.GetComponentsInChildren<SlotRefrigerator>(true).ToList();
        VegetableSlot = VegetableContent.transform.GetComponentsInChildren<SlotRefrigerator>(true).ToList();
        MeetSlot = MeetContent.transform.GetComponentsInChildren<SlotRefrigerator>(true).ToList();
        SlotDictionary.Add(1, FruitSlot);
        SlotDictionary.Add(2, VegetableSlot);
        SlotDictionary.Add(3, MeetSlot);
    }
    private void InputAllDataSlot(int type, List<SlotRefrigerator> slotArea)
    {
        foreach (SlotRefrigerator slot in slotArea)
        {
            int slotIndex = slotArea.FindIndex(s => s == slot);
            if (data.material[type].materialInfos.Count > slotIndex) //slot이 더 많을 경우 오류나지 않기 위해
            {
                int amount = data.material[type].materialInfos[slotIndex].Amount;
                slot.Type = type;
                slot.ID = data.material[type].materialInfos[slotIndex].ID;
                slot.SlotObject = data.material[type].materialInfos[slotIndex].PrefabMaterial;
                slot.SlotCount = amount;
                slot.RefrigeratorUI = this.gameObject;
            }
        }
    }
    private void InputDataSlot(int type,int id, int amount)
    {
        int slotFIndex = SlotDictionary[type].FindIndex(a => a.ID == id);
        SlotDictionary[type][slotFIndex].SlotCount = amount;
    }
    private void OnEnable() //테스트용
    {
        InputAllDataSlot(1, FruitSlot);
        InputAllDataSlot(2, VegetableSlot);
        InputAllDataSlot(3, MeetSlot);
    }
    public void InputRefrigerator(int type, int id, int amount)
    {
        data.ChangeAmount(type, id, amount++);
        InputDataSlot(type, id, amount++);
    }
    public void OpenRefrigerator()
    {
        refrigeratorUI.SetActive(true);
        InputAllDataSlot(1, FruitSlot);
        InputAllDataSlot(2, VegetableSlot);
        InputAllDataSlot(3, MeetSlot);
    }
    public void CloseRefrigerator()
    {
        refrigeratorUI.SetActive(false);
    }

}
/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public GameObject refrigeratorUI;
    public MaterialData data;
    public GameObject FruitContent;
    private SlotRefrigerator[] FruitSlot;
    public GameObject VegetableContent;
    private SlotRefrigerator[] VegetableSlot;
    public GameObject MeetContent;
    private SlotRefrigerator[] MeetSlot;

    private void Awake()
    {
        FruitSlot = FruitContent.transform.GetComponentsInChildren<SlotRefrigerator>(true);
        VegetableSlot = VegetableContent.transform.GetComponentsInChildren<SlotRefrigerator>(true);
        MeetSlot = MeetContent.transform.GetComponentsInChildren<SlotRefrigerator>(true);
    }
    private void InputDataSlot(int type, SlotRefrigerator[] slotArea)
    {
        foreach (SlotRefrigerator slot in slotArea)
        {
            int slotIndex = Array.IndexOf(slotArea, slot);
            if (data.material[type].materialInfos.Count > slotIndex) //slot�� �� ���� ��� �������� �ʱ� ����
            {
                int amount = data.material[type].materialInfos[slotIndex].Amount;
                slot.Type = type;
                slot.Order = slotIndex;
                slot.SlotCount = amount;
            }
        }
    }
    private void OnEnable() //�׽�Ʈ �� ����
    {
        InputDataSlot(0, FruitSlot);
        InputDataSlot(1, VegetableSlot);
        InputDataSlot(2, MeetSlot);
    }
    public void OpenRefrigerator()
    {
/*        refrigeratorUI.SetActive(true);
        LoadState(0, FruitSlot);
        LoadState(1, VegetableSlot);
        LoadState(2, MeetSlot);*/
    }
    public void CloseRefrigerator()
    {
        refrigeratorUI.SetActive(false);
    }
}
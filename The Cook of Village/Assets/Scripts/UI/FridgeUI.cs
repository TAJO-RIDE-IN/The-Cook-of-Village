using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeUI : SlotParent
{
    [SerializeField]
    private List<SlotFridge> FridgeSlot = new List<SlotFridge>();

    public override void LoadSlotData()
    {
        int count = 0;
        IngredientsType[] materialType = IngredientsData.Instance.IngredientsType;
        for (int i = 1; i < materialType.Length; i++)
        {
            foreach (IngredientsInfos materialInfos in materialType[i].IngredientsInfos)
            {
                if (SlotDictionary[FridgeSlot[count].transform.parent.name] == materialInfos.Type) //Type이 같을 때만 정보 Load
                {
                    FridgeSlot[count].ingredientsInfos = materialInfos;
                    FridgeSlot[count].SlotCount = materialInfos.Amount;
                    FridgeSlot[count].FridgeUI = this;
                }
                count++;
            }
        }
    }

    public void InputRefrigerator(int type, int id, int amount)
    {
        IngredientsData.Instance.ChangeAmount(type, id, amount++);
    }
    public override void OpenUI()
    {
        GameManager.Instance.IsUI = true;
        this.gameObject.SetActive(true);
        LoadSlotData();
    }
    public override void CloseUI()
    {
        GameManager.Instance.IsUI = false;
        this.gameObject.SetActive(false);
    }
}

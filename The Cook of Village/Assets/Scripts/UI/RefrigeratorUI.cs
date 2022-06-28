using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorUI : SlotParent
{
    [SerializeField]
    private List<SlotRefrigerator> RefrigeratorSlot = new List<SlotRefrigerator>();

    public override void LoadSlotData()
    {
        int count = 0;
        IngredientsType[] materialType = IngredientsData.Instance.IngredientsType;
        for (int i = 1; i < materialType.Length; i++)
        {
            foreach (IngredientsInfos materialInfos in materialType[i].IngredientsInfos)
            {
                if (SlotDictionary[RefrigeratorSlot[count].transform.parent.name] == materialInfos.Type) //Type이 같을 때만 정보 Load
                {
                    RefrigeratorSlot[count].ingredientsInfos = materialInfos;
                    RefrigeratorSlot[count].SlotCount = materialInfos.Amount;
                    RefrigeratorSlot[count].refrigeratorUI = this;
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

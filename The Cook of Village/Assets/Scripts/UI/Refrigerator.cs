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

public class Refrigerator : SlotParent
{
    public GameObject RefrigeratorUI;
    [SerializeField]
    private List<SlotRefrigerator> RefrigeratorSlot = new List<SlotRefrigerator>();

    private void Awake()
    {
        RefrigeratorSlot = RefrigeratorUI.transform.GetComponentsInChildren<SlotRefrigerator>(true).ToList();
    }
    public override void LoadSlotData()
    {
        int count = 0;
        MaterialType[] materialType = MaterialData.Instance.materialType;
        for(int i = 1; i < materialType.Length; i++)
        {
            foreach (MaterialInfos materialInfos in materialType[i].materialInfos)
            {
                if (SlotDictionary[RefrigeratorSlot[count].transform.parent.name] == materialInfos.Type) //Type이 같을 때만 정보 Load
                {
                    RefrigeratorSlot[count].materialInfos = materialInfos;
                    RefrigeratorSlot[count].SlotCount = materialInfos.Amount;
                    RefrigeratorSlot[count].RefrigeratorUI = RefrigeratorUI;
                }
                count++;
            }
        }
    }

    public void InputRefrigerator(int type, int id, int amount)
    {
        MaterialData.Instance.ChangeAmount(type, id, amount++);
    }
    public override void OpenUI()
    {
        GameManager.Instance.IsUI = true;
        RefrigeratorUI.SetActive(true);
        LoadSlotData();
    }
    public override void CloseUI()
    {
        GameManager.Instance.IsUI = false;
        RefrigeratorUI.SetActive(false);
    }
}
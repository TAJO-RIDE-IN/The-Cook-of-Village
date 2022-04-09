/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRefrigerator : MonoBehaviour
{
    public int Type;
    public int Order;
    public Text CountText;
    [SerializeField]
    private int slotCount;
    public int SlotCount
    {
        get { return slotCount; }
        set
        {
            slotCount = value;
            ModifyText();
            SlotState();
            Data.ChangeAmount(Type, Order, slotCount);
        }
    }
    private MaterialData Data;
    private void Awake()
    {
        Data = GameObject.FindGameObjectWithTag("GameController").GetComponent<MaterialData>();
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        bool state;
        state = (slotCount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    private void ModifyText()
    {
        CountText.text = "X" + SlotCount;
    }
    public void ClickSlot()
    {
        if (slotCount > 0)
        {
            SlotCount--;
        }
    }
}

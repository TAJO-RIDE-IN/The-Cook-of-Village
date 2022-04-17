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
    public Text CountText;
    private Transform Player;
    public int SlotCount
    {
        get { return materialInfos.Amount; }
        set
        {
            materialInfos.Amount = value;
            ModifySlot();
            SlotState();
            MaterialData.Instance.ChangeAmount(materialInfos.Type, materialInfos.ID, materialInfos.Amount);
        }
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnEnable()
    {
        SlotState();
    }
    private void SlotState()
    {
        bool state;
        state = (materialInfos.Amount > 0) ? true : false;
        this.gameObject.SetActive(state);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + SlotCount;
    }
    public override void SelectSlot()
    {
        if (materialInfos.Amount > 0)
        {
            SlotCount--;
            RefrigeratorUI.SetActive(false);
            Vector3 position = Player.position;
            Instantiate(materialInfos.PrefabMaterial, position, Quaternion.identity);
            Player.transform.GetComponent<CookingCharacter>().currentMaterial = materialInfos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public int Type;
    public int Order;
    public Text CountText;
    [SerializeField]
    private int SlotCount;
    private MaterialData Data;

    private void Awake()
    {
        Data = GameObject.FindGameObjectWithTag("GameController").GetComponent<MaterialData>();
    }

    private void State()
    {
        if(SlotCount == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        SlotCount = Data.material[Type].materialInfos[Order].Amount;
        ModifyText();
        State();
    }

    public void ModifyText()
    {
        CountText.text = "X" + SlotCount;
    }

}

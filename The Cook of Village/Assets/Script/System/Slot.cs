/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public MaterialInfos materialInfos;
    protected MaterialData Data;
    private void Awake()
    {
        Data = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MaterialData>();
    }
    public abstract void SelectSlot();
    public abstract void ModifySlot();
}

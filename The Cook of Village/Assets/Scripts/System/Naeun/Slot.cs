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
    public IngredientsInfos ingredientsInfos;
    public abstract void SelectSlot();
    public abstract void ModifySlot();
}

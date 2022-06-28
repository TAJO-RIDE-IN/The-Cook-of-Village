/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotParent : MonoBehaviour
{
    protected Dictionary<string, int> SlotDictionary = new Dictionary<string, int>()
    {
        {"Fruit", 1},
        {"Vegetable", 2},
        {"Meat", 3}
    };

    public abstract void OpenUI();
    public abstract void CloseUI();
    public abstract void LoadSlotData();
}

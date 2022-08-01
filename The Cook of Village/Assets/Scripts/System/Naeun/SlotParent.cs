using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotParent : MonoBehaviour
{
    public abstract void OpenUI();
    public abstract void CloseUI();
    public abstract void LoadSlotData();
}

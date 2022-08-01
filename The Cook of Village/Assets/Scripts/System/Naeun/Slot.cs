using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot<T> : MonoBehaviour
{
    public T Infos;
    public abstract void SelectSlot();
    public abstract void ModifySlot();
}

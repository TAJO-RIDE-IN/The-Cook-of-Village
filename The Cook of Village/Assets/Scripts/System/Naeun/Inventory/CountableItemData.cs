using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItemData : MonoBehaviour
{
    public int MaxAmount => _maxAmount;
    [SerializeField] private int _maxAmount = 99;
}

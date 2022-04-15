using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    public Text Money;
    public void ChangeMoney(int money)
    {
        Money.text = money.ToString();
    }
}

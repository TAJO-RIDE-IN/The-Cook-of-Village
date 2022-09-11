using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{

    private enum State { Deposit, Withdrawal }
    [SerializeField] private State CurrentState;

    private int ChangeInterestDay = 3;
    public InputField InputMoneyText;
    public void BankButtonClick()
    {
        GameData.Instance.Money += MoneyValue();
        GameData.Instance.BankMoney -= MoneyValue();
    }
    private int MoneyValue()
    {
        int _money = Int32.Parse(InputMoneyText.text);
        _money = (CurrentState == State.Deposit) ? -_money : _money; //입금 : 출금
        return _money;
    }
    public void MaxMoneyText()
    {
        int MaxMoney = (CurrentState == State.Deposit) ? GameData.Instance.Money : GameData.Instance.BankMoney;
        if (InputMoneyText.text != null)
        {
            int _money = Mathf.Clamp(Int32.Parse(InputMoneyText.text), 0, MaxMoney);
            InputMoneyText.text = _money.ToString();
        }
    }
}

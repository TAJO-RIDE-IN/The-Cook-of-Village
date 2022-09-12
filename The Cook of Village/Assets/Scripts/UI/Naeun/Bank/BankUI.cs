using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankUI : UIController
{
    private enum State { Deposit, Withdrawal }
    [SerializeField] private State CurrentState;
    public InputField InputMoneyText;

    public void UIState(bool state)
    {
        this.gameObject.SetActive(state);
        if (state) { LoadData(); }
    }

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
    private void LoadData()
    {

    }
    public void MaxMoneyText()
    {
        if (InputMoneyText.text != null)
        {
            int MaxMoney = (CurrentState == State.Deposit) ? GameData.Instance.Money : GameData.Instance.BankMoney;
            int _money = Mathf.Clamp(Int32.Parse(InputMoneyText.text), 0, MaxMoney);
            InputMoneyText.text = _money.ToString();
        }
    }
}

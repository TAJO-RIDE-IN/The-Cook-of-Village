using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private int ChangeInterestDay = 3;
    public void MoneyWithdrawal(int money)
    {
        GameData.Instance.BankMoney -= money;
        GameData.Instance.Money += money;
    }
    public void MoneyDeposit(int money)
    {
        GameData.Instance.BankMoney += money;
        GameData.Instance.Money -= money;
    }
}

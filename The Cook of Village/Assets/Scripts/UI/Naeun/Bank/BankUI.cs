using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BankUI : UIController
{
    public enum Service { Deposit, Withdrawal } //입금, 출금
    [SerializeField] private Service service;
    public Service CurrentService
    {
        get { return service; }
        set
        {
            service = value;
            LoadData();
        }
    }
    public InputField MoneyInputField;
    public Text BankServiceButtonText;
    public Text BankMoneyText;
    public Text BankInterestText;
    public void UIState(bool state)
    {
        this.gameObject.SetActive(state);
        if (state) { LoadData(); }
    }

    private int MoneyValue()
    {
        int _money = Int32.Parse(ReplaceMoneyText(MoneyInputField.text));
        _money = (CurrentService == Service.Deposit) ? -_money : _money; //입금 : 출금
        return _money;
    }
    private void LoadData()
    {
        BankMoneyText.text = MoneyText(MoneyData.Instance.BankMoney) + "원";
        BankInterestText.text = (MoneyData.Instance.BankInterest * 100).ToString() + "%";
        MoneyInputField.text = "";
        switch (CurrentService)
        {
            case Service.Deposit:
                BankServiceButtonText.text = "입금하기";
                break;
            case Service.Withdrawal:
                BankServiceButtonText.text = "출금하기";
                break;
        }
    }
    public void ChangeService(int state)
    {
        CurrentService = (Service)state;
    }

    public void BankButtonClick()
    {
        MoneyData.Instance.Money += MoneyValue();
        MoneyData.Instance.BankMoney -= MoneyValue();
        LoadData();
    }

    public void TotalMoneyButtonClick()
    {
        MoneyInputField.text = MoneyText(MaxMoney());
    }

    private string MoneyText(int _money) //숫자 콤마 표시
    {
        return _money.ToString("N0");
    }
    private string ReplaceMoneyText(string _text) //문자열에서 숫자만 얻기
    {
        string StrMoney = Regex.Replace(_text, @"[^0-9]", "");
        return StrMoney;
    }

    public void MoneyTextRange() //입력 범위 조절
    {
        string _text = ReplaceMoneyText(MoneyInputField.text);
        if (MoneyInputField.text != "" && _text != "")
        {
            int _money = Mathf.Clamp(Int32.Parse(_text), 0, MaxMoney());
            MoneyInputField.text = MoneyText(_money);
            return;
        }
        MoneyInputField.text = _text;
    }
    private int MaxMoney() //최대 금액
    {
        int MaxMoney = (CurrentService == Service.Deposit) ? MoneyData.Instance.Money : MoneyData.Instance.BankMoney;
        return MaxMoney;
    }
}

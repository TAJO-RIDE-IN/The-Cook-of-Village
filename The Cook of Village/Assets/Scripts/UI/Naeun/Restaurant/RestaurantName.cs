using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantName : UIController
{
    public GameObject NameChangeUI;
    public Text NameText;
    public InputField NameInput;
    public void Start()
    {
        NameText.text = GameData.Instance.RestaurantName;
    }
    /// <summary>
    /// RestaurantNameUI를 켜거나 끌 수 있다.
    /// 현재상태가 활성화 상태인 경우 비활성화 됨. 반대의 경우는 활성화 됨.
    /// </summary>
    public void RestaurantNameUIState(bool value)
    {
        NameChangeUI.SetActive(value);
    }
    public void InputText()
    {
        NameText.text = NameInput.text;
    }

    /// <summary>
    /// 이름을 바꾸거나 바꾸는걸 취소함
    /// </summary>
    /// <param name="use">true = 이름을 바꾼다. false = 이름을 바꾸지 않는다.</param>
    public void NameUIButton(bool use)
    {
        if(use)
        {
            GameData.Instance.RestaurantName = NameInput.text;
        }
        else
        {
            NameText.text = GameData.Instance.RestaurantName;
        }
        NameChangeUI.gameObject.SetActive(false);
    }
}

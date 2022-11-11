using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantName : MonoBehaviour
{
    public GameObject NameChangeUI;
    public Text NameText;
    public InputField NameInput;
    public void Start()
    {
        NameText.text = GameData.Instance.RestaurantName;
    }
    /// <summary>
    /// RestaurantNameUI�� �Ѱų� �� �� �ִ�.
    /// ������°� Ȱ��ȭ ������ ��� ��Ȱ��ȭ ��. �ݴ��� ���� Ȱ��ȭ ��.
    /// </summary>
    public void RestaurantNameUIState()
    {
        NameChangeUI.SetActive(!NameChangeUI.activeSelf);
    }
    public void InputText()
    {
        NameText.text = NameInput.text;
    }

    /// <summary>
    /// �̸��� �ٲٰų� �ٲٴ°� �����
    /// </summary>
    /// <param name="use">true = �̸��� �ٲ۴�. false = �̸��� �ٲ��� �ʴ´�.</param>
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

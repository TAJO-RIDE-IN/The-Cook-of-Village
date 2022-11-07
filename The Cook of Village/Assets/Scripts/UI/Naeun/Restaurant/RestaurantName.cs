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
    public void InputText()
    {
        NameText.text = NameInput.text;
    }
    public void ChangeButton()
    {
        GameData.Instance.RestaurantName = NameInput.text;
        NameChangeUI.gameObject.SetActive(false);
    }
    public void CancelButton()
    {
        NameText.text = GameData.Instance.RestaurantName;
        NameChangeUI.gameObject.SetActive(false);
    }
}

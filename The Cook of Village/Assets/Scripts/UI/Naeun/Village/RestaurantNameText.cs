using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantNameText : MonoBehaviour
{
    public Text NameText;
    private void Start()
    {
        NameText.text = GameData.Instance.RestaurantName;
    }
}

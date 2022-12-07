using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingButton : MonoBehaviour
{
    public void EndingButtonClick()
    {
        GameData.Instance.Fame = 450;
        GameData.Instance.TimeOfDay = 1320;
        MoneyData.Instance.Money = 550000;
    }
}

/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderControl : ObjectPooling<OrderUI>
{
    public Text OverOrderCount;
    private int orderCount;
    public int OrderCount
    {
        get { return orderCount; }
        set
        {
            orderCount = value;
            if(orderCount > objectpoolCount)
            {
                OverOrderCount.gameObject.SetActive(true);
                ChangeText();
            }
            else
            {
                OverOrderCount.gameObject.SetActive(false);
            }
        }
    }

    private void ChangeText()
    {
        int OverCount = orderCount - objectpoolCount;
        OverOrderCount.text = "+ " + OverCount.ToString();
    }
}

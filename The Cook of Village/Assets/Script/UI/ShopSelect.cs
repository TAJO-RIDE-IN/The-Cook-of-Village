/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSelect : MonoBehaviour
{
    public int Price;
    public Text TotalPrice;
    public Text CountText;
    public Text NameText;
    public Image SelectImgae;
    public Slider CountSlider;
    public MaterialData data;

    public void CloseSelcetSlot()
    {
        this.gameObject.SetActive(false);
    }

    private int CalculatePrice(int count)
    {
        int price = count * Price;
        return price;
    }

    public void ChangeSelctText()
    {
        CountText.text = CountSlider.value.ToString();
        TotalPrice.text = CalculatePrice((int)CountSlider.value).ToString();
    }

    #region ValueButtonClick

    #endregion
}

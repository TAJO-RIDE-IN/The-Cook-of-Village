using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMovement : MonoBehaviour
{

    public Transform UI;
    public Image foodOrderImage;
    public Sprite emptyFoodOrder;
    
    public void ShowUI()
    { 
        UI.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseUI()
    {
        UI.LeanMoveLocalY(725, 0.5f).setEaseOutExpo();
        StartCoroutine(ChangeUIEmpty());
    }

    IEnumerator ChangeUIEmpty()
    {
        yield return new WaitForSeconds(0.5f);
        foodOrderImage.sprite = emptyFoodOrder;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPosition : MonoBehaviour
{
    public int index;
    public bool isDirect;

    public GameObject CookPositionUI;
    public GameObject ovenObject;

    public GameObject noticeBox;
    public Text text;
    
    private ToolPooling _toolPooling;
    private ChefInventory _chefInventory;

    private void Start()
    {
        if (index == 6)
        {
            LeanTween.color(ovenObject, Color.clear, 0);
        }
        _toolPooling = ToolPooling.Instance;
        _chefInventory = ChefInventory.Instance;
        CookPositionUI.transform.localScale = Vector2.zero;
        CookPositionUI.SetActive(true);
    }

    public void OvenInstall()
    {
        if (ItemData.Instance.ItemType[6].ItemInfos[5].Amount == 1)
        {
            ToolPooling.Instance.toolInstallMode.GetAndPosition(index,"Oven");
            InstallData.Instance.PassIndexData(6,"Oven", InstallData.SortOfInstall.Tool);
            ItemData.Instance.ItemType[6].ItemInfos[5].Amount--;
            _chefInventory._cookingCharacter.isCookPositionCollider = false;
            isDirect = true;
            CloseUI(0.5f);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TextFade(noticeBox, text));
        }
    }

    public void DirectSetUp()
    {
        _toolPooling.toolInstallMode._cookingCharacter.isSpace = false;
        _toolPooling.toolInstallMode.DirectInstall();
        _toolPooling.indexToChange = index;
    }

    public void OpenUI(float time)
    {
        CookPositionUI.SetActive(true);
        UIManager.UIScalePunchAnimation(CookPositionUI);
        //CookPositionUI.LeanScale(Vector3.one, time).setEaseOutElastic();
        if (index == 6)//오븐이면 깜빡이는것
        {
            Debug.Log("오븐어쩌구");
            FadeInFadeOut();
        }
    }

    private void FadeInFadeOut()
    {
        LeanTween.color(ovenObject, new Color(1, 1, 1, 0.2f), 1)
            .setOnComplete(() => LeanTween.color(ovenObject, Color.clear, 1));
    }


    public void CloseUI(float time)
    {
        if (isDirect)
        {
            CookPositionUI.LeanScale(Vector2.zero, time).setEaseInBack().setOnComplete(() => AfterClose());
            isDirect = false;
            return;
        }
        
        CookPositionUI.SetActive(false);
        //CookPositionUI.LeanScale(Vector2.zero, time).setEaseInBack();
    }

    private void AfterClose()
    {
        CookPositionUI.SetActive(false);
        _chefInventory._cookingCharacter._cookingTool.OpenUI(0.5f);
    }
    public IEnumerator TextFade(GameObject box, Text text)
        {
            box.SetActive(true);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            while (text.color.a > 0.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 3.0f));
                yield return null;
            }
            box.SetActive(false);
            
        }
}

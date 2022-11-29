using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singletion<UIManager>
{
    public static List<GameObject> UIObject = new List<GameObject>();
    public static bool SubMenuActive = false;
    public static void UIState()
    {
        if (UIObject.Count >= 2)
        {
            UIObject[0].SetActive(false);
        }
        if (GameManager.Instance != null)
        {
            if(!SubMenuActive)
            {
                GameManager.Instance.IsUI = (UIObject.Count.Equals(0)) ? false : true;
            }
        }
    }

    public static void RemoveUseEsc()
    {
        UIObject[UIObject.Count - 1].SetActive(false);
    }

    public static void AddList(GameObject obj)
    {
        UIObject.Add(obj.gameObject);
        UIState();
    }
    public static void RemoveList(GameObject obj)
    {
        UIObject.Remove(obj.gameObject);
        UIState();
    }
    public static void SubMenuChangeisUI(bool state)
    {
        SubMenuActive = state;
        if (UIObject.Count.Equals(0))
        {
            GameManager.Instance.IsUI = state;
        }
    }
    public static Vector3 ZeroVector = new Vector3(0, 0, 0);
    public static Vector3 DefaultVector = new Vector3(1, 1, 1);
    public static void UIScalePingPongAnimation(GameObject obj)
    {
        LeanTween.scale(obj, new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setLoopPingPong();
    }
    public static void UIScalePunchAnimation(GameObject obj)
    {
        obj.transform.localScale = DefaultVector;
        LeanTween.scale(obj, new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setEasePunch();
    }
    public static void UIMoveAnimation(GameObject obj, Vector3 vector)
    {
        LeanTween.moveLocal(obj, vector, 0.1f);
    }
}

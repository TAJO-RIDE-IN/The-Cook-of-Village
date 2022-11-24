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
    public static void UIScalePingPongAnimation(GameObject obj, Vector3 vector3, float time)
    {
        LeanTween.scale(obj, vector3, time).setLoopPingPong();
    }
}

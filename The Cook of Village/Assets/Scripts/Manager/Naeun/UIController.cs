using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    protected static List<GameObject> UIObject = new List<GameObject>();

    protected void OnDisable()
    {
        RemoveList();
        Disable();
    }
    protected void OnEnable()
    {
        AddList();
        Enable();
    }

    protected virtual void Enable() {    }
    protected virtual void Disable() {    }
    private void UIState()
    {
        if (UIObject.Count >= 2)
        {
            UIObject[0].SetActive(false);
        }
        if(GameManager.Instance != null)
        {
            GameManager.Instance.IsUI = (UIObject.Count == 0) ? false : true;
        }
    }
    protected void AddList()
    {
        UIObject.Add(this.gameObject);
        UIState();
    }
    protected void RemoveList()
    {
        UIObject.Remove(this.gameObject);
        UIState();
    }
}

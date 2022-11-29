using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    protected void OnDisable()
    {
        if (UIManager.UIObject != null)
        {
            UIManager.RemoveList(this.gameObject);
        }
        Disable();
    }
    protected void OnEnable()
    {
        UIManager.UIOpenScaleAnimation(this.gameObject);
        UIManager.AddList(this.gameObject);
        Enable();
    }

    protected virtual void Enable() {    }
    protected virtual void Disable() {    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    public Text WarningText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWarning()
    {
        StartCoroutine(TextFadeOut());
    }

    public IEnumerator TextFadeOut()
    {
        //InvenWarning.SetActive(true);
        int loopNum = 0;
        //InvenWarning.color = Color.
        while (WarningText.color.a > 0.0f)
        {
            WarningText.color = new Color(WarningText.color.r, WarningText.color.g, WarningText.color.b, WarningText.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        //yield return null;
        if(loopNum++ > 10000)
            throw new System.Exception("Infinite Loop");
    }
}

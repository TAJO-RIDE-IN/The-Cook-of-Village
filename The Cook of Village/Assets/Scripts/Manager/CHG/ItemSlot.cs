using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = Microsoft.Unity.VisualStudio.Editor.Image;

public class ItemSlot : MonoBehaviour
{
    private int index;//필요없을지두..?

    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }

    private UnityEngine.UI.Image slotUI;
    

    public bool isBeingUsed;
    // Start is called before the first frame update
    void Start()
    {
        slotUI = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSlotUI(Sprite sprite)
    {
        slotUI.sprite = sprite;
    }
}

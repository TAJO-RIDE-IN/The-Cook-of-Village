using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    private int index;
    
    public int Index
    {
        get { return index;}
        set
        {
            index = value;
            //EdibleItems.Add();
        }
    }

    public bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

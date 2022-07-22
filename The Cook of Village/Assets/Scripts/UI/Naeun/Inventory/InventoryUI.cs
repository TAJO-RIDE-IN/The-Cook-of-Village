using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public enum Tab { Potion, Utensils, Furniture}
    public Tab CurrentTab = Tab.Potion;

    public void TabState(Tab tab)
    {
        CurrentTab = tab;
        switch(CurrentTab)
        {
            case Tab.Potion:
                break;
            case Tab.Utensils:
                break;
            case Tab.Furniture:
                break;
        }
    }


}

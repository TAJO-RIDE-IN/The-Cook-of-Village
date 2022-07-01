using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public ShopUI shopUI;
    public ShopUI.Shop type;
    public bool isShop = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentShop();
            isShop = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isShop = false;
        }
    }

    private void currentShop()
    {
        shopUI.shop = type;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public ShopUI shopUI;
    public enum Type { FruitShop, VegetableShop, MeatShop }
    public Type type;
    public bool isShop = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentShop();
            shopUI.OpenShop();
            isShop = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isShop = false;
            shopUI.CloseShop();
        }
    }

    private void currentShop()
    {
        switch(type)
        {
            case Type.FruitShop:
                shopUI.shop = ShopUI.Shop.FruitShop;
                break;
            case Type.VegetableShop:
                shopUI.shop = ShopUI.Shop.VegetableShop;
                break;
            case Type.MeatShop:
                shopUI.shop = ShopUI.Shop.MeatShop;
                break;
        }
    }
}

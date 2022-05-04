using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public ShopUI shopUI;
    public enum Type { Fruit, Vegetable, Meat }
    public Type type;
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
        switch(type)
        {
            case Type.Fruit:
                shopUI.shop = ShopUI.Shop.Fruit;
                break;
            case Type.Vegetable:
                shopUI.shop = ShopUI.Shop.Vegetable;
                break;
            case Type.Meat:
                shopUI.shop = ShopUI.Shop.Meat;
                break;
        }
    }
}

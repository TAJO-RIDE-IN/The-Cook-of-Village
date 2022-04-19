using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCharacter : MonoBehaviour
{
    private bool isShopCollider;

    private ShopNPC _shopNpc;
    private void Update()
    {
        if (isShopCollider)
        {
            InTheShop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Shop"))
        {
            isShopCollider = true;
            _shopNpc = other.transform.GetComponent<ShopNPC>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            _shopNpc.shopUI.CloseShop();
        }
        isShopCollider = false;
    }
    
    private void InTheShop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shopNpc.shopUI.OpenShop();
        }
    }
}

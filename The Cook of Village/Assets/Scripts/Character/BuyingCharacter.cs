using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BuyingCharacter : MonoBehaviour
{
    private bool isShopCollider;
    public CinemachineFreeLook cinemachine;

    private ShopNPC _shopNpc;

    private void Start()
    {
        cinemachine = cinemachine.GetComponent<CinemachineFreeLook>();
    }

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
            cinemachine.m_XAxis.m_MaxSpeed = 300;
            cinemachine.m_YAxis.m_MaxSpeed = 2;
        }
        isShopCollider = false;
    }
    
    private void InTheShop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shopNpc.shopUI.OpenShop();
            cinemachine.m_XAxis.m_MaxSpeed = 0;
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
    }
}

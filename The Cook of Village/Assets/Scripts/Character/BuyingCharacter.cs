using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BuyingCharacter : MonoBehaviour
{
    private bool isShopCollider;
    public CinemachineFreeLook cinemachine;

    private VillageNPC _npc;

    private void Start()
    {
        cinemachine = cinemachine.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (isShopCollider)//상점말고 esc메뉴 눌렀을때도 화면 움직이면 안되니깐 isUI 넣기로함
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _npc.UIState(true);
            }
        }

        if(GameManager.Instance != null)
        {
            if (GameManager.Instance.IsUI)
            {
                StopMovingXYAxis();
            }
            else
            {
                MovingXYAxis();
            }
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("VillageNPC"))
        {
            isShopCollider = true;
            _npc = other.transform.GetComponent<VillageNPC>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "VillageNPC")
        {
            _npc.UIState(false);
        }
        isShopCollider = false;
    }
    
    private void InTheShop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _npc.UIState(true);
        }
    }

    public void StopMovingXYAxis()
    {
        cinemachine.m_XAxis.m_MaxSpeed = 0;
        cinemachine.m_YAxis.m_MaxSpeed = 0;
    }
    public void MovingXYAxis()
    {
        cinemachine.m_XAxis.m_MaxSpeed = 300;
        cinemachine.m_YAxis.m_MaxSpeed = 2;
    }
}

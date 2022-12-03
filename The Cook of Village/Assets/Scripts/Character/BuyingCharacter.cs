using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BuyingCharacter : MonoBehaviour
{
    public bool isShopCollider;
    private bool isRestNameCollider;
    public RestaurantNameUI _restaurantName;
    private VillageNPC _npc;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void WhenSpaceDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShopCollider)
            {
                _npc.UIState(true);
                return;
            }

            if (isRestNameCollider)
            {
                _restaurantName.RestaurantNameUIState(true);
            }
            
        }
    }

    private void Update()
    {
        if (isShopCollider || isRestNameCollider)//상점말고 esc메뉴 눌렀을때도 화면 움직이면 안되니깐 isUI 넣기로함
        {
            WhenSpaceDown();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VillageNPC"))
        {
            isShopCollider = true;
            _npc = other.transform.GetComponent<VillageNPC>();
        }
        if (other.gameObject.name == "RestaurantName")
        {
            isRestNameCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "VillageNPC")
        {
            _npc.UIState(false);
            isShopCollider = false;
            return;
        }
        
        if (other.name == "RestaurantName")
        {
            //레스토랑 이름 UI 끄기
            _restaurantName.RestaurantNameUIState(false);
            isRestNameCollider = false;
            return;
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    public GameObject fridgeInven;
    public GameObject potInven;
    public GameObject panInven;
    public GameObject blenderInven;
    public bool isUI = false;
    public Animator frigdeAnimator;

    public Sprite Lemon;

    
    private void OnTriggerEnter(Collider other)
    {
        //Inven.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fridge")
        {
            if (Input.GetKey(KeyCode.E))
            {
                frigdeAnimator.SetBool("isOpen",true);
                fridgeInven.SetActive(true);
                isUI = true;
                if (!isUI)
                {
                    
                }
                else
                {
                    Debug.Log("꺼짐");
                    
                    //Inven.SetActive(false);
                    //isUI = false;
                }
            }
        }

        if (other.tag == "Pot")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                potInven.SetActive(true);
                Image image = potInven.transform.GetChild(0).GetComponent<Image>();
                image.sprite = Lemon;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fridge")
        {
            frigdeAnimator.SetBool("isOpen",false);
            Debug.Log("꺼짐");
            fridgeInven.SetActive(false);
        }
    }
}

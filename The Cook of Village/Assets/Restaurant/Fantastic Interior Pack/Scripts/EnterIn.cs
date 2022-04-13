using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnterIn : MonoBehaviour
{
    public GameObject Inven;
    public bool isUI = false;
    public Animator frigdeAnimator;

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
                Debug.Log("완성");
                frigdeAnimator.SetBool("isOpen",true);
                Inven.SetActive(true);
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fridge")
        {
            frigdeAnimator.SetBool("isOpen",false);
            Debug.Log("꺼짐");
            Inven.SetActive(false);
        }
    }
}

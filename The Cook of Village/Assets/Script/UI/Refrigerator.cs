using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public GameObject refrigeratorUI;
    public MaterialData data;
    public GameObject[] SlotRefrigerator;


    private void Start()
    {
        SlotRefrigerator = GameObject.FindGameObjectsWithTag("Slot");
    }
    public void OpenRefrigerator()
    {
        refrigeratorUI.SetActive(true);
    }
    public void CloseRefrigerator()
    {
        refrigeratorUI.SetActive(false);
    }

}
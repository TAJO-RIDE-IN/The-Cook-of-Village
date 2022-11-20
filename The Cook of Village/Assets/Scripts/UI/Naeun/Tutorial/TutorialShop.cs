using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShop : MonoBehaviour
{
    public TutorialVillageController Controller;
    public ShopUI shop;

    public void Start()
    {
        if(GameManager.Instance.gameMode == GameManager.GameMode.Tutorial)
        {
            Controller.VillageTutorialUI.DialogueText();
        }
    }
}

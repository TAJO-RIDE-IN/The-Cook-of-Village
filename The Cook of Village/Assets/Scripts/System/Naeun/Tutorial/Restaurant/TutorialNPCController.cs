using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCController : NPCPooling
{
    public TutorialRestaurantController Controller;
    public TutorialGuestMove GuestNPC;
    private bool isOpen = true;

    private void Awake()
    {

    }
    public void GuestEnter()
    {
        GuestNPC.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.Instance.Play(SoundManager.Instance._audioClips["OpenRestaurant"]);
            openUI.LeanScale(Vector2.zero, 0.5f).setOnComplete(() => GuestEnter());
        }
    }
}

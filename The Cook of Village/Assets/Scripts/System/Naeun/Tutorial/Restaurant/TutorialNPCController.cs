using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCController : MonoBehaviour
{
    public TutorialRestaurantController tutorialController;
    public TutorialGuestMove GuestNPC;
    public GameObject openUI;
    public ChairUse Chair;

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

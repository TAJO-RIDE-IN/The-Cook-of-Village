using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCController : MonoBehaviour
{
    public TutorialRestaurantController tutorialController;
    public GuestNPC TutorialGuestNPC;
    public ChairUse Chair;

    public void GuestEnter()
    {
        TutorialGuestNPC.gameObject.SetActive(true);
        TutorialGuestNPC.ChangeState(GuestNPC.State.Walk);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.Instance.Play(SoundManager.Instance._audioClips["OpenRestaurant"]);
            GuestEnter();
            tutorialController.NextDialogue();
        }
    }
}

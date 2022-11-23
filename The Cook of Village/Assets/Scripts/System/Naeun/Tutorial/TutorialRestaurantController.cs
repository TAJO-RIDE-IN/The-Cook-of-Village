using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantController : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject VillageParticle;
    public TutorialRestaurantUI tutorialRestaurantUI;
    public ThirdPersonMovement Player;
    private void Start()
    {
        if (GameManager.Instance.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            gameManager = GameManager.Instance;
            Init();
        }
    }
    private void Init()
    {
        //Player.StopMoving();
        gameManager.TutorialUI = true;
        tutorialRestaurantUI.DialogueState(true);
        tutorialRestaurantUI.CallDialogue("Restaurant");
        VillageParticle.SetActive(false);
    }
}

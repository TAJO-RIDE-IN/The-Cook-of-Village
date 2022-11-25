using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialController : MonoBehaviour
{
    protected GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            Init();
        }
    }
    public abstract void Init();
    public abstract void NextDialogue();
    public virtual void EndEvent() { }
}

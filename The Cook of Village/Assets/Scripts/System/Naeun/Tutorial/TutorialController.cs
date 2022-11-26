using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialController : MonoBehaviour
{
    protected GameManager gameManager;
    protected DialogueManager dialogueManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = DialogueManager.Instance;
        if (gameManager.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            GameData.Instance.orbitSpeed = 0;
            Init();
        }
    }
    public virtual void PlayerControl(bool state, string name = "") { }
    public abstract void Init();
    public abstract void PlayAction(bool state);
    public abstract void NextDialogue();
    public virtual void EndEvent() { }
}

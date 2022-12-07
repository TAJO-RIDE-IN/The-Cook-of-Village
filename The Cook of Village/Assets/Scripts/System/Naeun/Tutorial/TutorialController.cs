using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialController : MonoBehaviour
{
    protected GameManager gameManager;
    protected DialogueManager dialogueManager;
    public GameObject InputManager;
    public PauseUI pauseUI;
    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = DialogueManager.Instance;
        if (gameManager.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            GameData.Instance.orbitSpeed = 0;
            InputManager.SetActive(false);
            StartCoroutine(Esc());
            Init();
        }
    }

    private IEnumerator Esc()
    {
        while(pauseUI != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseUI.PauseUIState(!pauseUI.gameObject.activeSelf);
            }
            yield return null;
        }
    }
    private void Update()
    {

    }
    public virtual void PlayerControl(bool state, string name = "") { }
    public abstract void Init();
    public abstract void PlayAction(bool state);
    public abstract void NextDialogue();
    public virtual void EndEvent() { }
}

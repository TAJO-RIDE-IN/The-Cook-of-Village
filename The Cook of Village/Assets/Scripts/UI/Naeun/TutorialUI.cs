using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour, IDialogue
{
    public Text SentenceText;
    public GameObject NextButton;
    public GameObject ChoiceButton;
    private DialogueManager dialogueManager;
    private DialogueData.ContentType type = DialogueData.ContentType.Tutorial;
    private bool GoTutorial;
    private void DialogueState(bool state)
    {
        this.gameObject.SetActive(state);
        if(!state)
        {
            if(GoTutorial)
            {
                GetComponent<ChangeScene>().MoveScene();
                GoTutorial = false;
            }
        }
    }
    private void ButtonState(bool state)
    {
        NextButton.SetActive(!state);
        ChoiceButton.SetActive(state);
    }
    public void YesButton()
    {
        GoTutorial = true;
        DialogueText();
    }
    public void CallDialogue(string name)
    {
        dialogueManager = DialogueManager.Instance;
        dialogueManager.CallDialogue(type, name);
        DialogueState(true);
        DialogueText();
    }
    public void DialogueText(int answer = 0)
    {
        (string, bool, bool) Dialogue = dialogueManager.Dialogue(answer);
        if(Dialogue.Item3)
        {
            DialogueState(false);
        }
        ButtonState(Dialogue.Item2);
        dialogueManager.TypingEffet(SentenceText, Dialogue.Item1);
    }
}

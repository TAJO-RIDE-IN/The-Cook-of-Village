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

    private void DialogueState(bool state)
    {
        this.gameObject.SetActive(state);
    }
    private void ButtonState(bool state)
    {
        NextButton.SetActive(!state);
        ChoiceButton.SetActive(state);
    }
    public void CallDialogue(string name)
    {
        dialogueManager = DialogueManager.Instance;
        dialogueManager.CallDialogue(type, name);
    }
    public void StartTutorial()
    {
        DialogueState(true);
        CallDialogue("WhetherQuestion");
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
        SentenceText.text = Dialogue.Item1;
    }
}

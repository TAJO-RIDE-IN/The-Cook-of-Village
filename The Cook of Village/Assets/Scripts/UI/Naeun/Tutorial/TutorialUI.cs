using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour, IDialogue
{
    public Text SentenceText;
    public Button NextButton;
    public Button YesButton;
    public Button NoButton;
    public GameObject ChoiceButton;
    private DialogueManager dialogueManager;
    private DialogueData.ContentType type = DialogueData.ContentType.Tutorial;

    private void Awake()
    {
        AddButtonListener();
    }
    protected virtual void AddButtonListener()
    {
        NextButton.onClick.AddListener(() => DialogueText());
        YesButton.onClick.AddListener(() => DialogueText(0));
        NoButton.onClick.AddListener(() => DialogueText(1));
    }
    public void DialogueState(bool state)
    {
        this.gameObject.SetActive(state);
        if(!state)
        {
            Disable();

        }
    }
    protected virtual void Disable() { }
    private void ButtonState(bool QuestionState, bool Stay)
    {
        bool NextButtonState = (QuestionState) ? false : !Stay;
        NextButton.gameObject.SetActive(NextButtonState);
        ChoiceButton.SetActive(QuestionState);
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
        (string, bool, bool, bool) Dialogue = dialogueManager.Dialogue(answer);
        if(Dialogue.Item3)
        {
            DialogueState(false);
        }
        ButtonState(Dialogue.Item2, Dialogue.Item3);
        dialogueManager.TypingEffet(SentenceText, Dialogue.Item1);
    }
}

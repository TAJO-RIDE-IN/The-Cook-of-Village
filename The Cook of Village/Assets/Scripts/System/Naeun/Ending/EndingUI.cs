using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public Text SentenceText;
    public Text NameText;
    public GameObject Name;
    public GameObject NextButton;
    private bool canNext = true;
    public bool CanNext
    {
        get { return canNext; }
        set
        {
            canNext = value;
            NextButton.SetActive(CanNext);
        }
    }
    public EndingController endingController;
    private DialogueManager dialogueManager;
    private DialogueData.ContentType type = DialogueData.ContentType.Ending;
    private void Update()
    {
        if(CanNext)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueText();
            }
        }
    }
    private void DialogueState(bool state)
    {
        this.gameObject.SetActive(state);
    }
    private string[] ModifySentence(string sentence)
    {
        string[] NewSentence = { sentence, "" };
        if(sentence.Contains("@"))
        {
            NewSentence = sentence.Split('@');
        }
        return NewSentence;
    }
    private void Action(bool action)
    {
        if(action)
        {
            endingController.Action();
        }
    }
    private void NameCheck(string name)
    {
        Name.SetActive(!name.Equals(""));
        NameText.text = name;
    }
    public void CallDialogue(string name)
    {
        dialogueManager = DialogueManager.Instance;
        dialogueManager.CallDialogue(type, name);
        DialogueState(true);
        DialogueText();
    }
    public void DialogueText()
    {
        (string, bool, bool, bool) Dialogue = dialogueManager.Dialogue();
        string[] newSentence = ModifySentence(Dialogue.Item1);
        dialogueManager.TypingEffect(SentenceText, newSentence[0]);
        NameCheck(newSentence[1]);
        Action(Dialogue.Item4);
        DialogueState(!Dialogue.Item3);
    }
}

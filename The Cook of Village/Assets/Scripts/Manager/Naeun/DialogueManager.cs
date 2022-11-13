using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestionSentence
{
    [TextArea] public string Question;
    [TextArea] public string[] AnswerSentence;
}
[System.Serializable]
public class DialogueContent
{
    public string SentenceName;
    [TextArea] public string[] Sentence;
    public QuestionSentence[] questionSentence;
}
[System.Serializable]
public class DialogueData
{
    public enum ContentType {Tutorial, Ending};
    [SerializeField]public ContentType type;
    public DialogueContent[] dialogueContents;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private DialogueData[] DialogueData;
    [HideInInspector]public DialogueContent CurrentUseDialogue;
    private int DialogueIndex;
    public void CallDialogue(DialogueData.ContentType type, string SentenceName)
    {
        foreach(var content in DialogueData[(int)type].dialogueContents)
        {
            if (content.SentenceName == SentenceName)
            {
                CurrentUseDialogue = content;
            }
        }
    }
    public void NextDialogue(int QuestionNum = 0)
    {
        if(CurrentUseDialogue != null)
        {
            return;
        }
        string sentence = CurrentUseDialogue.Sentence[DialogueIndex];

    }
/*    private void DialogueControl(string sentence, int num)
    {
        if(sentence.Contains("&PlayerName"))
        {
            sentence.Replace("&PlayerName", GameData.Instance.PlayerName);
        }
        if (sentence.Contains("&(question"+"num"+")"))
        {
            sentence = CurrentUseDialogue.questionSentence[num].Question;
        }
    }*/
    private string ReplaceSentence(string sentence)
    {
        string str = sentence.Replace("&PlayerName", GameData.Instance.PlayerName);
        str = sentence.Replace("&(question0)", GameData.Instance.PlayerName);
        return str;
    }
}

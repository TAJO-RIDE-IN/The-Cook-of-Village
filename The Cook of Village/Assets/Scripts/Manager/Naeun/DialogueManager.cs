using System;
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

public interface IDialogue
{
    void CallDialogue(string name);
}

public class DialogueManager : Singletion<DialogueManager>
{
    [SerializeField]private DialogueData[] DialogueData;
    [HideInInspector]public DialogueContent CurrentUseDialogue;
    [HideInInspector]public Queue<string> CurrentSentences = new Queue<string>();
    private int QuestionNum;
    private bool Question = false;
    private bool NextEnd = false;

    /// <summary>
    /// �̿��ϰ���� DiloagueContent�� �ҷ��´�.
    /// </summary>
    /// <param name="type">Tutorial, Ending</param>
    /// <param name="SentenceName">DialogueContent�� SentenceName �Է�</param>
    public void CallDialogue(DialogueData.ContentType type, string SentenceName)
    {
        CurrentUseDialogue = null;
        CurrentSentences = new Queue<string>();
        Question = false;
        NextEnd = false;
        QuestionNum = 0;
        foreach (var content in DialogueData[(int)type].dialogueContents)
        {
            if (content.SentenceName == SentenceName)
            {
                CurrentUseDialogue = content;
                foreach (var text in content.Sentence)
                {
                    CurrentSentences.Enqueue(text);
                }
            }
        }
    }
    /// <summary>
    /// CallDialogue���� �ҷ��� DialogueContent�� ��縦 �ҷ��´�.
    /// �ҷ��� ������ ���� ��縦 �����´�.
    /// </summary>
    /// <param name="answer">���� ������ ������ ��� ������ �� index �Է�, �⺻ �� 0</param>
    /// <returns>DialogueContent��  Sentence, Question�� ��� true</returns>
    public (string,bool,bool) Dialogue(int answer = 0)
    {
        if (CurrentUseDialogue != null)
        {
            string sentence;
            if (!Question)
            {
                sentence = CurrentSentences.Dequeue();
            }
            else
            {
                sentence = CurrentUseDialogue.questionSentence[QuestionNum].AnswerSentence[answer];
                QuestionNum++;
            }
            sentence = ReplaceSentence(sentence);
            return (sentence, Question, NextEnd);
        }
        return ("", Question, NextEnd);
    }

    private void SentenceState()
    {
        
    }

    /// <summary>
    /// ���忡�� �ٲ��� �ϴ� �κ�(�÷��̾��̸�, ����)�� �ٲ��ش�.
    /// </summary>
    /// <param name="sentence">��ȯ�ϰ� ���� ����</param>
    /// <returns>��ȯ�� ����</returns>
    private string ReplaceSentence(string sentence)
    {
        Question = false;
        string replace = sentence.Replace("&PlayerName", GameData.Instance.PlayerName);
        if (sentence.Contains("&(question" + QuestionNum + ")"))
        {
            replace = CurrentUseDialogue.questionSentence[QuestionNum].Question;
            Question = true;
        }
        else if(sentence.Contains("&(End)"))
        {
            NextEnd = true;
        }
        return replace;
    }
}

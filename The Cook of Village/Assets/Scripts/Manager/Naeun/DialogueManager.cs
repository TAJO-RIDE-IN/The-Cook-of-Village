using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public enum ContentType { Tutorial, Ending };
    [SerializeField] public ContentType type;
    public DialogueContent[] dialogueContents;
}

public interface IDialogue
{
    void CallDialogue(string name);
}

public class DialogueManager : Singletion<DialogueManager>
{
    [SerializeField] private DialogueData[] DialogueData;
    [HideInInspector] public DialogueContent CurrentUseDialogue;
    [HideInInspector] public Queue<string> CurrentSentences = new Queue<string>();
    private int QuestionNum;
    private bool Question = false;
    private bool NextEnd = false;
    private Coroutine TypingCorutine;
    private void ResetState()
    {
        CurrentSentences.Clear();
        CurrentUseDialogue = null;
        Question = false;
        NextEnd = false;
        QuestionNum = 0;
    }

    /// <summary>
    /// �̿��ϰ���� DiloagueContent�� �ҷ��´�.
    /// </summary>
    /// <param name="type">Tutorial, Ending</param>
    /// <param name="SentenceName">DialogueContent�� SentenceName �Է�</param>
    public void CallDialogue(DialogueData.ContentType type, string SentenceName)
    {
        ResetState();
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
    /// <returns>DialogueContent��  Sentence, Question�� ��� true, ���� ������ ������� true</returns>
    public (string, bool, bool) Dialogue(int answer = 0)
    {
        if (!NextEnd)
        {
            string sentence;
            if (!Question) //�������� �ƴ��� Ȯ��
            {
                sentence = CurrentSentences.Dequeue();
            }
            else
            {
                sentence = CurrentUseDialogue.questionSentence[QuestionNum].AnswerSentence[answer];
                QuestionNum++;
            }
            sentence = ReplaceSentence(sentence);
            return (sentence, Question, false);
        }
        return ("", Question, NextEnd);
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
        if (sentence.Contains("&(Question" + QuestionNum + ")"))
        {
            replace = CurrentUseDialogue.questionSentence[QuestionNum].Question;
            Question = true;
        }
        else if (sentence.Contains("&(End)"))
        {
            replace = sentence.Replace("&(End)", "");
            NextEnd = true;
        }
        return replace;
    }

    public void TypingEffet(Text _text, string _sentence)
    {
        if (TypingCorutine != null)
        {
            StopCoroutine(TypingCorutine);
        }
        TypingCorutine = StartCoroutine(TypeSentence(_text, _sentence));
    }
    private IEnumerator TypeSentence(Text _text, string _sentence)
    {
        _text.text = null;
        foreach (var letter in _sentence)
        {
            _text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

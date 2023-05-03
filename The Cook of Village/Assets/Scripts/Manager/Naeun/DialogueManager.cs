using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public enum ContentType { Tutorial, Ending , Explanation };
    [SerializeField] public ContentType type;
    public DialogueContent[] dialogueContents;
}

public interface IDialogue
{
    void CallDialogue(string name);
}

public class DialogueManager : Singletion<DialogueManager>
{
    public DialogueData[] DialogueData;
    [HideInInspector] public DialogueContent CurrentUseDialogue;
    [HideInInspector] public Queue<string> CurrentSentences = new Queue<string>();
    [HideInInspector] public string CurrentSentencesName;
    private int QuestionNum;
    private bool Question = false;
    private bool NextEnd = false;
    private bool EndUse = false;
    private bool Action = false;
    private Coroutine TypingCorutine;
    private void ResetState()
    {
        CurrentSentences.Clear();
        CurrentUseDialogue = null;
        Question = false;
        NextEnd = false;
        EndUse = false;
        Action = false;
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
        CurrentSentencesName = SentenceName;
        foreach (var content in DialogueData[(int)type].dialogueContents)
        {
            if (content.SentenceName == SentenceName)
            {
                CurrentUseDialogue = content;
                foreach (var text in content.Sentence)
                {
                    CurrentSentences.Enqueue(text);
                }
                return;
            }
        }
    }
    /// <summary>
    /// CallDialogue���� �ҷ��� DialogueContent�� ��縦 �ҷ��´�.
    /// �ҷ��� ������ ���� ��縦 �����´�.
    /// </summary>
    /// <param name="answer">���� ������ ������ ��� ������ �� index �Է�, �⺻ �� 0</param>
    /// <returns>DialogueContent��  Sentence, Question�� ��� true, ���� ������ ������� true</returns>
    public (string, bool, bool, bool) Dialogue(int answer = 0)
    {
        if (!NextEnd || EndUse)
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
            return (sentence, Question, false, Action);
        }
        EndUse = true;
        return ("", false, NextEnd, false);
    }

    private string Replace;
    /// <summary>
    /// ���忡�� �ٲ��� �ϴ� �κ�(�÷��̾��̸�, ����)�� �ٲ��ش�.
    /// </summary>
    /// <param name="sentence">��ȯ�ϰ� ���� ����</param>
    /// <returns>��ȯ�� ����</returns>
    private string ReplaceSentence(string sentence)
    {
        Question = false;
        Action = false;
        EndUse = false;
        NextEnd = false;
        Replace = "";
        Replace = sentence.Replace("&(PlayerName)", GameData.Instance.PlayerName);
        if (sentence.Contains("&(Question" + QuestionNum + ")"))
        {
            Replace = CurrentUseDialogue.questionSentence[QuestionNum].Question;
            Question = true;
        }
        if (sentence.Contains("&(Action)"))
        {
            Replace = Replace.Replace("&(Action)", "");
            Action = true;
        }
        if (sentence.Contains("&(End)"))
        {
            Replace = Replace.Replace("&(End)", "");
            NextEnd = true;
        }
        return Replace;
    }

    #region TypingEffet
    private StringBuilder stringBuilder = new StringBuilder();
    private bool isColorRich = false;
    private string InsertText;
    private int InsertIndex;
    private int EndRichIndex;
    public void TypingEffect(Text _text, string _sentence)
    {
        isColorRich = false;
        if (TypingCorutine != null)
        {
            StopCoroutine(TypingCorutine);
        }
        isColorRich = _sentence.Contains("</color>");
        TypingCorutine = StartCoroutine(TypeSentence(_text, _sentence));
    }
    private void RichText(string _sentence, string current, int start) // <Color=cyan></color> stringBuilder�� ����
    {
        string RichText = "";
        bool check = false;
        int count = 0;
        InsertText = "";
        InsertIndex = 0;
        EndRichIndex = 0;
        for (int i = start; i < _sentence.Length; i++) //�ҷ��� ���忡�� ���� ã�� ����
        {
            if (_sentence[i].Equals('<') || check) //<Color=cyan>�� <Ȯ��
            {
                check = true;
                RichText += _sentence[i];
                if (_sentence[i].Equals('>')) //<Color=cyan>�� >Ȯ��
                {
                    check = false;
                    count++;
                    if (count.Equals(1)) // �����ؾ��ϴ� �ε��� ����
                    {
                        InsertIndex = i + 1;
                    }
                }
            }
            else
            {
                InsertText += _sentence[i];
            }
            if (count.Equals(2)) // <> <> ���·� 2�� �ֱ� ������ 2�� ã�� �� for�� Ż��
            {
                EndRichIndex = i;
                stringBuilder = new StringBuilder(current + RichText);
                break;
            }
        }
    }

    private IEnumerator TypeSentence(Text _text, string _sentence)
    {
        _text.text = null;
        bool AddRich = false;
        string insert = "";
        for(int i = 0; i < _sentence.Length; i++)
        {
            if (_sentence[i].Equals('<') && isColorRich) // Rich�� ù ���� �κ� ����
            {
                AddRich = true;
                RichText(_sentence, _text.text, i); // <Color=cyan></color> stringBuilder�� ����
            }
            if(AddRich)
            {
                _text.text = stringBuilder.Insert(InsertIndex, _sentence[InsertIndex]).ToString();
                insert += _sentence[InsertIndex];
                InsertIndex++;
                if (insert.Equals(InsertText))
                {
                    i = EndRichIndex;
                    AddRich = false;
                }
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                _text.text += _sentence[i];
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    #endregion
}

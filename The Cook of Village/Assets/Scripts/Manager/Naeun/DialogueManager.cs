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
    /// 이용하고싶은 DiloagueContent를 불러온다.
    /// </summary>
    /// <param name="type">Tutorial, Ending</param>
    /// <param name="SentenceName">DialogueContent의 SentenceName 입력</param>
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
    /// CallDialogue에서 불러온 DialogueContent의 대사를 불러온다.
    /// 불러올 때마다 다음 대사를 가져온다.
    /// </summary>
    /// <param name="answer">이전 문장이 질문인 경우 질문의 답 index 입력, 기본 값 0</param>
    /// <returns>DialogueContent의  Sentence, Question인 경우 true, 다음 문장이 없을경우 true</returns>
    public (string, bool, bool) Dialogue(int answer = 0)
    {
        if (!NextEnd)
        {
            string sentence;
            if (!Question) //질문인지 아닌지 확인
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
    /// 문장에서 바뀌어야 하는 부분(플레이어이름, 질문)을 바꿔준다.
    /// </summary>
    /// <param name="sentence">변환하고 싶은 문장</param>
    /// <returns>변환된 문장</returns>
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

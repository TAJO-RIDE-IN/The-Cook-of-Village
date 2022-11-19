using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartUI : TutorialUI
{
    public ChangeScene changeScene;
    private bool GoTutorial;
    protected override void Disable()
    {
        if (GoTutorial)
        {
            changeScene.MoveScene();
            GameManager.Instance.gameMode = GameManager.GameMode.Tutorial;
            GoTutorial = false;
        }
    }
    protected override void AddButtonListener()
    {
        NextButton.onClick.AddListener(() => DialogueText());
        YesButton.onClick.AddListener(() => YesButtonClick());
        NoButton.onClick.AddListener(() => DialogueText(1));
    }
    public void YesButtonClick()
    {
        GoTutorial = true;
        DialogueText();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAnimation : MonoBehaviour
{
    private Animator _animator;
    public ChangeScene changeScene;

    private void Start()
    {
        _animator = transform.GetComponent<Animator>();
    }

    public void AfterAni()
    {
        GameData.Instance.SetTimeMorning();
        _animator.SetBool("isSleep", false);
        GameData.Instance.isPassOut = true;
        changeScene.MoveScene();
        if (GameManager.Instance.NextSceneIndex == 2)
        {
            //GameManager.Instance.
            //씬 이동해주고, 씬 이동 끝나면 애니메이션 재생, 침대 앞으로 이동
        }

        if (GameManager.Instance.NextSceneIndex == 3)
        {
            //애니메이션 재생, 침대 앞으로 이동, 
        }
    }
}

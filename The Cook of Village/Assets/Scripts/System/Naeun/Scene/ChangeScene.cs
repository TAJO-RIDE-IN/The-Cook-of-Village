using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public enum SceneToGo { Intro, SceneLoad, Village, Restaurant }
    public SceneToGo sceneToGo;

    private bool isMove;

    private void Start()
    {
        isMove = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isMove)
                {
                    MoveScene();
                    isMove = true;
                }
                
            }
        }
    }

    public void MoveScene()
    {
        GameManager.Instance.NextSceneIndex = (int)sceneToGo;
        GameManager.Instance.LoadObject();
        SceneManager.LoadSceneAsync(1);
    }
}

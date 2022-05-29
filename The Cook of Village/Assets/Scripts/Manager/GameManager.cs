/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }    
    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public bool IsUI = false;
    public int NextSceneIndex = 2;

    public void Pause() // Game Pause
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}

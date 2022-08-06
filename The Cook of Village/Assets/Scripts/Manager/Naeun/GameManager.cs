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
            LoadObject();
            CurosrControl(true);
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

    private bool isUI = false;
    public bool IsUI
    {
        get { return isUI; }
        set 
        {
            isUI = value;
            //CurosrControl(value);
        }
    }
    public int NextSceneIndex = 2;
    public int CurrentSceneIndex;

    #region OnSceneLoaded
    private void Start()
    {
        LoadObject();
    }
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadObject();
    }
    private void LoadObject()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    #endregion
    private void CurosrControl(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = (value) ?  CursorLockMode.None : CursorLockMode.Locked;
    }
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

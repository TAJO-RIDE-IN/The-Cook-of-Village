using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletion<GameManager>
{
    public enum GameMode {Default, Tutorial}
    [SerializeField]private GameMode mode;
    public GameMode gameMode
    {
        get { return mode; }
        set 
        {
            mode = value;
            if (mode == GameMode.Default)
            {
                gameData.orbitSpeed = 2;
            }
            else
            {
                gameData.orbitSpeed = 0;
            }
        }
    }
    [SerializeField] private GameData gameData;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Potion potionController;
    protected override void Init()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        CurrentSceneName = SceneManager.GetActiveScene().name;
        LoadObject();
        DontDestroyOnLoad(this.gameObject);
        CursorInit();
    }

    private void CursorInit()
    {
        if (currentSceneIndex == 0)
        {
            CursorControl(true);
        }
        if (currentSceneIndex == 1)
        {
            CursorControl(true);
        }
        if (currentSceneIndex == 2)
        {
            //CursorControl(false);
        }

        if (currentSceneIndex == 3)
        {
            CursorControl(true);
        }
    }

    private bool isOpen = false;
    public bool IsOpen
    {
        get { return isOpen; }
        set 
        {
            isOpen = value;
        }
    }
    
    private bool isInstall = false;

    public bool IsInstall
    {
        get { return isInstall; }
        set 
        {
            isInstall = value;
        }
    }

    [SerializeField] private bool tutorialUI = false;
    public bool TutorialUI
    {
        get { return tutorialUI; }
        set
        {
            tutorialUI = value;
            IsUI = value;
            //CurosrControl(value);
        }
    }

    [SerializeField] private bool isUI = false;
    public bool IsUI
    {
        get { return isUI; }
        set 
        {
            if(TutorialUI)
            {
                value = true;
            }
            isUI = value;
            //CurosrControl(value);
        }
    }

    public int NextSceneIndex = 2;
    private int currentSceneIndex;
    public int CurrentSceneIndex
    {
        get { return currentSceneIndex; }
        set
        {
            currentSceneIndex = value;
        }
    }
    public string CurrentSceneName;

    #region OnSceneLoaded
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
    public void LoadObject()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        CurrentSceneName = SceneManager.GetActiveScene().name;
        if (soundManager._audioClips.Count != 0)
        {
            soundManager.SceneLoadSound(CurrentSceneName);
        }
        Pause(false);
        SceneInitControl(CurrentSceneIndex);
        isOpen = false;
    }
    #endregion

    private void SceneInitControl(int index)
    {
        potionController.LoadObject();
        gameData.LoadObject();
        switch (index)
        {
            case 0: //시작
                CursorControl(true);
                break;
            case 1: //로딩
                CursorControl(true);
                break;
            case 2: //마을
                potionController.VillageSceneInit();
                gameData.PlaySceneInit();
                //CursorControl(false);
                break;
            case 3: //레스토랑
                potionController.RestaurantSceneInit();
                gameData.PlaySceneInit();
                CursorControl(true);
                break;
        }
    }

    public void CursorControl(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = (value) ? CursorLockMode.None :  CursorLockMode.Locked;
    }
    public void Pause(bool state) // Game Pause
    {
        Time.timeScale = state ? 0.0f : 1.0f;
        if(state)
        {
            soundManager.MuteSound(1, true);
            soundManager.MuteSound(2, true);
        }
        else
        {
            soundManager.MuteSound(1, soundManager.EffectMute);
            soundManager.MuteSound(2, soundManager.EffectMute);
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}

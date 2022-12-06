using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public interface IGameManagerOb
{
    void AddObserver(IObserver<GameManager> o);
    void RemoveObserver(IObserver<GameManager> o);
    void NotifyObserver(List<IObserver<GameManager>> observer);
}
public class GameManager : Singletion<GameManager>, IGameManagerOb
{
    private List<IObserver<GameManager>> observers = new List<IObserver<GameManager>>();
    public enum GameMode {Default, Tutorial, Ending}
    [SerializeField]private GameMode mode;
    public GameMode gameMode
    {
        get { return mode; }
        set 
        {
            mode = value;
            if (mode.Equals(GameMode.Default))
            {
                gameData.orbitSpeed = 2;
            }
            else
            {
                gameData.orbitSpeed = 0;
                NotifyObserver(observers);
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
    }

    [SerializeField] private bool isOpen = false;
    public bool IsOpen
    {
        get { return isOpen; }
        set 
        {
            isOpen = value;
        }
    }
    
    [SerializeField]private bool isInstall = false;

    public bool IsInstall
    {
        get { return isInstall; }
        set 
        {
            isInstall = value;
            if(value)
            {
                isInstall = false;
                NavMeshBaker.NavMeshBake();
            }    
        }
    }

    private bool tutorialUI = false;
    public bool TutorialUI
    {
        get { return tutorialUI; }
        set
        {
            tutorialUI = value;
            IsUI = value;
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
            CursorControl(value);
        }
    }

    public int NextSceneIndex;
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
        IsUI = false;
        UIManager.SubMenuActive = false;
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
                CursorControl(false);
                break;
            case 3: //레스토랑
                potionController.RestaurantSceneInit();
                gameData.PlaySceneInit();
                CursorControl(true);
                break;
        }
    }

    private void CursorControl(bool value)
    {
        if(currentSceneIndex.Equals(1) || currentSceneIndex.Equals(2))
        {
            Cursor.visible = value;
            Cursor.lockState = (value) ? CursorLockMode.None : CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Pause(bool state) // Game Pause
    {
        Time.timeScale = state ? 0.0f : 1.0f;
    }

    public void GameQuit()
    {
        gameData.SaveDataTime("ExitSave");
        Application.Quit();
    }

    public void AddObserver(IObserver<GameManager> o)
    {
        observers.Add(o);
    }
    public void RemoveObserver(IObserver<GameManager> o)
    {
        observers.Remove(o);
    }
    public void NotifyObserver(List<IObserver<GameManager>> observer)
    {
        foreach(var obs in observers)
        {
            obs.Change(this);
        }
    }
}

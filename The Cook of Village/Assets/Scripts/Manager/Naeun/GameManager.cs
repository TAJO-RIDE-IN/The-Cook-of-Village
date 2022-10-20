using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    [SerializeField] private GameData gameData;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Potion potionController;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if(null == instance)
        {
            instance = this;
            CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            CurrentSceneName = SceneManager.GetActiveScene().name;
            LoadObject();
            //CurosrControl(true);
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

    [SerializeField] private bool isUI = false;
    public bool IsUI
    {
        get { return isUI; }
        set 
        {
            isUI = value;
            //CurosrControl(value);
        }
    }
    [SerializeField] private int guestCount;
    public int GuestCount
    {
        get { return guestCount; }
        set
        {
            guestCount = value;
        }
    }
    public int NextSceneIndex = 2;
    public int CurrentSceneIndex;
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
    private void LoadObject()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        CurrentSceneName = SceneManager.GetActiveScene().name;
        Pause(false);
        potionController.LoadObject();
        gameData.LoadObject();
        if(soundManager._audioClips.Count != 0)
        {
            soundManager.SceneLoadSound(CurrentSceneName);
        }
        if (GuestCount != 0)
        {
            GameData.Instance.ChangeFame(-3 * GuestCount);
        }
        GuestCount = 0;
    }
    #endregion
    private void CurosrControl(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = (value) ? CursorLockMode.None :  CursorLockMode.Locked;
    }
    public void Pause(bool state) // Game Pause
    {
        Time.timeScale = state ? 0.0f : 1.0f;
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}

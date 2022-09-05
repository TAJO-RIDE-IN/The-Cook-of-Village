using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region singleton
    private static UIManager instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Init()
    {
        UIObject = GameObject.FindGameObjectsWithTag("UI").ToList();
    }    
    #endregion
    [SerializeField] private List<GameObject> UIObject = new List<GameObject>();


}

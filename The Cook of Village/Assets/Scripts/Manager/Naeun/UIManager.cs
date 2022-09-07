using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public interface IUIObject
{
    public void AddUIList();
    public void RemoveUIList();
}
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
 
    #endregion
    [SerializeField] private List<GameObject> _UIObject = new List<GameObject>();
    public List<GameObject> UIObject
    {
        get { return _UIObject; }
        set
        {
            _UIObject = value;
            if(_UIObject.Count >= 2)
            {
                _UIObject[0].SetActive(false);
            }
        }
    }


}

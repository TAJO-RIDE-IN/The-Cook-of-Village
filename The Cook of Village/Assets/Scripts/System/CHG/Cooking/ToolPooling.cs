using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class ToolPooling : ObjectPooling<CookingTool>
{
    // Start is called before the first frame update
    public Transform[] transforms;
    public CookingTool[] cookingTool;
    private Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();
    private GameObject[] ObjectContatiner = new GameObject[5];
    public Queue<CookingTool> queue = new Queue<CookingTool>();

    [Serializable]
    public struct ToolBox
    {
        public CookingTool _cookingTool;
        public Queue<CookingTool> toolQueue;
    }

    public ToolBox[] _toolBox;
    
    private int selectedToolIndex;
    public int SelectedToolIndex
    {
        get
        {
            return selectedToolIndex;
        }
        set
        {
            selectedToolIndex = value;
        }
    }
    
    private int _selectedPositionIndex;
    public int SelectedPositionIndex
    {
        get
        {
            return _selectedPositionIndex;
        }
        set
        {
            _selectedPositionIndex = value;
        }
    }

    public void ReceiveToolIndex(int x)//조리도구 사용한다에 할당
    {
        SelectedToolIndex = x;
    }


    public void ReceivePositionIndex(int x)//UI 클릭에 할당, SelectedToolIndex에 따라서 조리도구 풀해주기
    {
        SelectedPositionIndex = x;
    }
    
    
    
    protected override void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        for (int i = 0; i < _toolBox.Length; i++)
        {
            ObjectContatiner[i] = transform.GetChild(i).gameObject;
        }
        
        
        Initialize(objectpoolCount);
    }

    protected override void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolQueue.Enqueue(CreateNewObject());
        }
    }

    protected override CookingTool CreateNewObject()
    {
        var newObj = Instantiate(PoolObject, transform);
        newObj.gameObject.SetActive(false);
        return newObj;
        return base.CreateNewObject();
    }

}

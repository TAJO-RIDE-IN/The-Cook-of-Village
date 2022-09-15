using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using KeyType = System.String;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();

    
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

}

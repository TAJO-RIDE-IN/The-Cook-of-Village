using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ToolPooling : MultipleObjectPooling<CookingTool>
{
    //private Dictionary<String, List<PoolObjectData>> pooledObjects = new Dictionary<String, List<PoolObjectData>>();
    public Transform[] toolPosition;
    public ToolInstallMode toolInstallMode;
    

    /// <summary>
    /// ?ฐ๋ ?ด์ : ?ค์ด?ํธ ์ฒด์ธ์ง?์ ?ฌ์ฉ. CookingTool??DirectSetUp?์ ๋ฐ๊? ?๋ฆฌ ?์น ?ธ๋ฑ?ค๋? ?๊ฒจ์ค????๋?? ?ด๊ฑธ ?ฌ์ฉ?๋ ?จ์??Use???์????คํฌ๋ฆฝํธ?์ ?ฌ์ฉ?๊ธฐ ?๋ฌธ?ด๋ค.
    /// ?ํ cookingCharacter.cookingTool.index? ๊ฐ์? ๊ฐ์ด??
    /// </summary>
    public int indexToChange;
    
    public ItemInfos selectedItemInfos;

    private String selectedToolName;
    public String SelectedToolName
    {
        get
        {
            return selectedToolName;
        }
        set
        {
            selectedToolName = value;
        }
    }
    
    private int selectedPositionIndex;
    public int SelectedPositionIndex
    {
        get
        {
            return selectedPositionIndex;
        }
        set
        {
            selectedPositionIndex = value;
        }
    }

    /// <summary>
    /// ?์??ด๊? ??ฅํ ?์ด???ฐ์ด?ฐ์??๊ฐ์ ๋นผ์ฃผ๊ณ??ํด์ฃผ๊ธฐ ?ํจ
    /// </summary>
    public int SelectedToolID;

    private static ToolPooling instance;
    public static ToolPooling Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        selectedPositionIndex = -1;
        
    }

    
    public void FalseDirect()
    {
        toolInstallMode.isDirectChange = false;
    }
    public void ChangeToolAmount(int count, FoodTool.Type type)
    {
        ItemData item = ItemData.Instance;
        ItemInfos infos = item.ToolIdToItem((int)type);
        item.ChangeAmount(infos.ID, count);
    }


    


    
    
    

}

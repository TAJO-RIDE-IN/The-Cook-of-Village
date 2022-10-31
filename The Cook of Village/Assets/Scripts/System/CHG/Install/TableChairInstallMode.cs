using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChairInstallMode : MonoBehaviour
{
    public int installableTableCount;
    public int installableChairCount;
    
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    
    private void Start()
    {
        isUsedTable = new bool[installableTableCount];
        isUsedChair = new bool[installableChairCount];
        
        //데이터에 따라 시작 할 때 설치
        for (int i = 0; i < InstallData.toolData._indexNames.Count; i++)
        {
            //GetAndPosition(InstallData.toolData._indexNames[i].index, InstallData.toolData._indexNames[i].name);
        }
    }
    
}

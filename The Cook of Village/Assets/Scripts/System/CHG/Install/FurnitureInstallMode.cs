using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInstallMode : InstallMode
{
    public int installableTableCount;
    public int installableChairCount;
    public int installableDecoCount;
    
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    private bool[] isUsedDeco;
    
    private void Start()
    {
        isUsedTable = new bool[installableTableCount];
        isUsedChair = new bool[installableChairCount];
        isUsedDeco = new bool[installableDecoCount];
        //데이터에 따라 시작 할 때 설치
        for (int i = 0; i < InstallData.toolData._indexNames.Count; i++)
        {
            //GetAndPosition(InstallData.toolData._indexNames[i].index, InstallData.toolData._indexNames[i].name);
        }
    }

    public override void ReceivePositionIndex(int x)
    {
        
    }

    protected override void ReturnPooledObject()
    {
        
    }

    public override void GoInstall()
    {
        
    }

    protected override void StartInstall()
    {
        
    }

    public override void CancelInstall()
    {
        
    }
    
}

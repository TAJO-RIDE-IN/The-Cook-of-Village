using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추가해야하는 변수: isUsed 배열, installableTCount 정수
/// </summary>
public abstract class InstallMode : MonoBehaviour
{
    public GameObject goInstallUI;
    public GameObject cancelInstallUI;
    public GameObject[] toolPositionUI;
    public InventoryUI inventoryUI;
    
    
    public List<int> receivedPositionList = new List<int>();
    
    public int toolItemInfosAmount;
    
    /// <summary>
    /// 이 값을 리스트에 넣어도 되는지
    /// </summary>
    protected bool isList;
    
    /// <summary>
    /// 설치하고 싶은 자리의 선택된 개수
    /// </summary>
    public int selectedToolAmount;


    public abstract void ReceivePositionIndex(int x);
    
    public virtual void GetAndPosition(int index, string name){}
    
    public virtual void Use(string name, int amount){}

    protected abstract void Return();

    public abstract void GoInstall();

    protected abstract void StartInstall();

    public abstract void CancelInstall();
}



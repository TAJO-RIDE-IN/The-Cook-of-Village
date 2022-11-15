using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추가해야하는 변수: isUsed 배열, installableTCount 정수
/// </summary>
public abstract class InstallMode : MonoBehaviour
{
    
    
    /// <summary>
    /// 이 값을 리스트에 넣어도 되는지
    /// </summary>
    protected bool isList;
    
    /// <summary>
    /// 설치하고 싶은 자리의 선택된 개수
    /// </summary>
    


    public virtual void ReceivePositionIndex(int x){}
    
    public virtual void GetAndPosition(int index, string name){}
    
    public virtual void Use(ItemInfos itemInfos){}

    protected virtual void ReturnPooledObject(){}

    public virtual void GoInstall(){}

    protected virtual void StartInstall(){}

    public virtual void CancelInstall(){}
}



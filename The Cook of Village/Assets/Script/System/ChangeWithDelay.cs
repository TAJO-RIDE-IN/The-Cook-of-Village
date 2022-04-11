/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeWithDelay
{
    //delay ���� true�� �ٲ���
    public static IEnumerator CheckDelay<T>(this T origin, T changeValue, float delay, Action<T> makeResult)
    {
        yield return new WaitForSeconds(delay);
        makeResult(changeValue);
    }
}

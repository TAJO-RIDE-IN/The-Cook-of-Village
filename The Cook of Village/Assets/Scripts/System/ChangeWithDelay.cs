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
    //delay ���� �Լ� ����
    public static IEnumerator CheckDelay(float delay, Action makeResult)
    {
        yield return new WaitForSeconds(delay);
        makeResult();
    }
}

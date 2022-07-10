using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeWithDelay
{
    //delay 이후 함수 실행
    public static IEnumerator CheckDelay(float delay, Action makeResult)
    {
        yield return new WaitForSeconds(delay);
        makeResult();
    }
}

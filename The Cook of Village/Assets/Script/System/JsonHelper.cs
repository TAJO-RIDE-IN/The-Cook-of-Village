using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.array;
    }

    public static string arrayToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper,prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
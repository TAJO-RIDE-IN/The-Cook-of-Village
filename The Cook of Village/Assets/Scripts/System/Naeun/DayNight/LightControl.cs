using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour, IObserver<GameData>
{
    //public Light
    private void Start()
    {
        AddObserver(GameData.Instance);
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }
    public void RemoveObserver(IGameDataOb o)
    {
        if (o != null) { o.RemoveObserver(this); }
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour, IObserver<GameData>
{
    public List<Light> ObjectLight = new List<Light>();
    private void Start()
    {
        AddObserver(GameData.Instance);
    }

    [Range(0.8f, 2.5f)]
    public float LightIntensity;
    private bool isMoning = false;
    private bool isChange = false;
    private void ChangeIntensity()
    {
        LightIntensity += 0.001f;
        isChange = LightIntensity > 2.5f;
        ChangeLightBrightness();
    }
    private void ChangeLightBrightness()
    {
        foreach(var light in ObjectLight)
        {
            light.intensity = LightIntensity;
        }
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            if(obj.TimeOfDay > 1100 && obj.TimeOfDay < 1400 && !isChange)
            {
                isMoning = false;
                ChangeIntensity();
            }
            if(obj.TimeOfDay > 480 && obj.TimeOfDay < 1000 && !isMoning)
            {
                isMoning = true;
                isChange = false;
                LightIntensity = 0.8f;
                ChangeLightBrightness();
            }
        }
    }
}

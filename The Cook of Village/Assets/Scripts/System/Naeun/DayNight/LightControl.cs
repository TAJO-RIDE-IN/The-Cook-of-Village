using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour, IObserver<GameData>
{
    public List<Light> ObjectLight = new List<Light>();
    private void Start()
    {
        LightIntensity = MinLight;
        AddObserver(GameData.Instance);
    }
    public float lightStartTime = 1100;
    public float LightIntensity;
    public float Light = 0.005f;
    public float MinLight;
    public float MaxLight;
    private bool isMoning = false;
    private bool isChange = false;
    private void ChangeIntensity(float time)
    {
        float lightTime = time - lightStartTime;
        LightIntensity =  Light * lightTime;
        isChange = LightIntensity > MaxLight;
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
            if(obj.TimeOfDay > lightStartTime && obj.TimeOfDay < 1400 && !isChange)
            {
                isMoning = false;
                ChangeIntensity(obj.TimeOfDay);
            }
            if(obj.TimeOfDay > 480 && obj.TimeOfDay < lightStartTime && !isMoning)
            {
                isMoning = true;
                isChange = false;
                LightIntensity = MinLight;
                ChangeLightBrightness();
            }
        }
    }
}

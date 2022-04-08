using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SunLight : MonoBehaviour
{/*    public GameManager gameManager;
    [Range(0, 24)]
    public float TimeOfDay;
    public float orbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;

    private bool isNight;
    private PhysicallyBasedSky sky;
    // Start is called before the first frame update
    void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay += Time.deltaTime * orbitSpeed;
        gameManager.TimeOfDay = TimeOfDay;
        if (TimeOfDay > 24)
        {
            TimeOfDay = 0;
            gameManager.Day++;
        }*//*

        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();
    }
    private void UpdateTime()
    {
        float alpha = TimeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRataion = sunRotation - 180;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRataion, -150.0f, 0);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 25.0f;
        sky.spaceRotation.value = new Vector3(moonRataion, -150.0f, 0);
        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if(isNight)
        {
            if(moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }
    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.None;
    }*/
}

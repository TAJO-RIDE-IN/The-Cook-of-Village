using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Slider backVolume;
    public AudioSource audio;
    public Toggle toggle;
    private float backVol = 1f;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.RemoveAllListeners();
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        audio.volume = backVolume.value;
        toggle.onValueChanged.AddListener(Mute);
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
        if (backVolume.value == 0)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
    }

    public void Mute(bool _bool)
    {
     
        if (_bool == false)
        {
            backVolume.value = 0;
        }
        else if (_bool == true)
        {
            backVolume.value = 0.5f;
        }
    }
}

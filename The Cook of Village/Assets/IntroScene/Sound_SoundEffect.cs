using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound_SoundEffect : MonoBehaviour
{
    public AudioMixer masterMixer2;
    public Slider audioSlider;

    public void AudioControl() {
    float sound = audioSlider.value;

    if(sound == -40f) masterMixer2.SetFloat("SoundEffect",-80);
    else masterMixer2.SetFloat("SoundEffect",sound);
}

   public void ToggleAudioVolume()
{
AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
}
}
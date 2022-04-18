using UnityEngine;
using System.Collections;

public class Dropdowneffect : MonoBehaviour
{

    public AudioClip soundExplosion;
    AudioSource myAudio;
    public static Dropdowneffect instance;
    void Awake()
    {
        if (Dropdowneffect.instance == null)
            Dropdowneffect.instance = this;
    }
    // Use this for initialization

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        myAudio.PlayOneShot(soundExplosion);
    }

}
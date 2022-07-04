using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 곡의 이름.
    public AudioClip clip; // 곡.
}

[System.Serializable]
public class EffectType
{
    [SerializeField]
    public enum Type {Plyaer, GuestNPC, ShopNPC}
    public Type type = Type.Plyaer;
    public List<Sound> sound = new List<Sound>();
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    #region singleton
    void Awake() // 객체 생성시 최초 실행.
    {
        if (instance == null)
        {
            instance = this;
            if (_audioSource.isPlaying)
            {
                //return; //배경음악이 재생되고 있다면 패스
            }
            else
            {
                _audioSource.Play(); //배경음악 계속 재생하게(이후 버튼매니저에서 조작)
            }
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();


    public string[] playSoundName;

    [SerializeField]
    public List<EffectType> effectSounds = new List<EffectType>();
    public Sound[] bgmSounds;

    public AudioSource _audioSource;

    void Start()
    {
        _audioSource = transform.GetComponent<AudioSource>();
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySE(string _name)
    {
/*        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");*/
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }

}

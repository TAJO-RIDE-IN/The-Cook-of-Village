using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public SoundData.Type type = SoundData.Type.Effect;
    public string SoundName;
    public AudioClip _audio;
}
[System.Serializable]
public class SoundType
{
    public string Name;
    public List<Sound> sound = new List<Sound>();
}

[System.Serializable]
public class SoundData
{
    [SerializeField]
    public enum Type { Bgm, Effect, Effect3D }
    public Type type = Type.Effect;
    public List<SoundType> soundtype = new List<SoundType>();
}
[System.Serializable]
public class AudioSourecs
{
    [SerializeField] public SoundData.Type type = SoundData.Type.Bgm;
    public List<AudioSource> audioSources = new List<AudioSource>();
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public List<SoundData> _soundData = new List<SoundData>();
    public bool EffectMute;
    private static SoundManager instance = null;
    #region singleton
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            AudioDictionary();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion singleton
    [SerializeField]public AudioSourecs[] audioSources;
    public Dictionary<string, Sound> _audioClips = new Dictionary<string, Sound>();
    public Dictionary<GameObject, AudioSource> _3DAudio = new Dictionary<GameObject, AudioSource>();

    private void AudioDictionary()
    {
        foreach(var _soundType in _soundData)
        {
            foreach(var _sounds in _soundType.soundtype)
            {
                foreach (var _sound in _sounds.sound)
                {
                    _audioClips.Add(_sound.SoundName, _sound);
                }
            }
        }
    }
    private void Audio3DList()
    {
        _3DAudio.Clear();
        var audio3D = GameObject.FindGameObjectsWithTag("Effect3D");
        foreach (var audio in audio3D)
        {
            if (audio != null)
            {
                audioSources[(int)SoundData.Type.Effect3D].audioSources.Add(audio.GetComponent<AudioSource>());
                //_3DAudio.Add(audio.transform.parent.gameObject, audio.GetComponent<AudioSource>());
            }
        }
    }

    public void SceneLoadSound(string SceneName)
    {
        Audio3DList();
        string name = SceneName + "BGM";
        Play(_audioClips[name]);
    }

    public void Play(Sound sound, float pitch = 1.0f)
    {
        if (sound == null)
            return;
        var audioSource = audioSources[(int)sound.type].audioSources[0];
        audioSource.pitch = pitch;
        audioSource.clip = sound._audio;

        switch (sound.type)
        {
            case SoundData.Type.Bgm:
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.Play();

                break;
            case SoundData.Type.Effect:
                audioSource.PlayOneShot(sound._audio);
                break;
        }
    }
    public void PlayEffect3D(Sound sound, GameObject _object, bool isloop , float pitch = 1.0f) // true = loop, false = OneShot;
    {
        if (_object.transform.Find("Sound") == null)
        {
            GameObject _sound = new GameObject { name = "Sound"};
            _sound.transform.parent = _object.transform;
            _sound.AddComponent<AudioSource>();
            audioSources[(int)SoundData.Type.Effect3D].audioSources.Add(_sound.GetComponent<AudioSource>());
        }
        var audioSource = _object.transform.Find("Sound").gameObject.GetComponent<AudioSource>();
        audioSource.loop = isloop;
        audioSource.pitch = pitch;
        audioSource.clip = sound._audio;
        audioSource.Play();
    }
    public void StopEffect3D(GameObject _object)
    {
        var audioSource = _object.transform.Find("Sound").GetComponent<AudioSource>();
        audioSource.Stop();
    }
    public void AudioVolume(int type, float vloume)
    {
        foreach (var audio in audioSources[type].audioSources)
        {
            if (audio != null)
            {
                audio.volume = vloume;
            }
        }
    }

    public void MuteSound(int type, bool _mute)
    {
        foreach (var audio in audioSources[type].audioSources)
        {
            if (audio != null)
            {
                audio.mute = _mute;
            }
        }
    }

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (var audioType in audioSources)
        {
            foreach(var audio in audioType.audioSources)
            {
                audio.clip = null;
                audio.Stop();
            }
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }
}

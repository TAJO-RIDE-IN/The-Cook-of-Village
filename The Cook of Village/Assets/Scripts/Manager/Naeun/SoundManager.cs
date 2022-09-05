using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public SoundData.Type type = SoundData.Type.Effect;
    public string SoundName;
    public AudioClip audio;
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
    public enum Type { Bgm, Effect }
    public Type type = Type.Effect;
    public List<SoundType> soundtype = new List<SoundType>();
}

public class SoundManager : MonoBehaviour
{
    [ArrayElementTitle("soundtype")]
    [SerializeField]
    public List<SoundData> SoundData = new List<SoundData>();
    private static SoundManager instance = null;
    #region singleton
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            Init();
            DontDestroyOnLoad(this.gameObject);
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
    public AudioSource[] _audioSources = new AudioSource[System.Enum.GetValues(typeof(SoundData.Type)).Length];
    Dictionary<string, Sound> _audioClips = new Dictionary<string, Sound>();

    private void AudioDictionary()
    {
        foreach(var _soundType in SoundData)
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

    public void Init()
    {
        AudioDictionary();
        GameObject root = GameObject.Find("Sound");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundData.Type)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)global::SoundData.Type.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
        }
        Play(_audioClips["RestuarantBackground"]);
    }

    public void Play(Sound sound, float pitch = 1.0f)
    {
        if (sound == null)
            return;

        if (sound.type == global::SoundData.Type.Bgm) // BGM 배경음악 재생
        {
            var audioSource = _audioSources[(int)global::SoundData.Type.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = sound.audio;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            var audioSource = _audioSources[(int)global::SoundData.Type.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(sound.audio);
        }
    }

    public void AudioVolume(SoundData.Type type, float vloume)
    {
        _audioSources[(int)type].volume = vloume;
    }

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }
}

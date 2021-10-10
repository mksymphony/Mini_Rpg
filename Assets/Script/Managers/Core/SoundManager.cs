using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _AudioSource = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClip = new Dictionary<string, AudioClip>();
    // MP3 PLAYER -> AudioSource
    // MP3 À½¿ø   -> AudioClip
    // °ü°´(±Í)   -> AudioListner

    public void Init()
    {
        GameObject Root = GameObject.Find("@Sound");
        if (Root == null)
        {
            Root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(Root);

            string[] SoundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < SoundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = SoundNames[i] };
                _AudioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = Root.transform;
            }
            _AudioSource[(int)Define.Sound.BGM].loop = true;
        }
    }
    public void Play(string Path, Define.Sound type = Define.Sound.Effect, float Pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(Path, type);
        Play(audioClip, type, Pitch);
    }
    public void Play(AudioClip audio, Define.Sound type = Define.Sound.Effect, float Pitch = 1.0f)
    {
        if (audio == null)
            return;

        if (type == Define.Sound.BGM)
        {

            AudioSource audioSource = _AudioSource[(int)Define.Sound.BGM];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.pitch = Pitch;
            audioSource.clip = audio;
            audioSource.Play();
            //TODO
        }
        else
        {
            AudioSource audioSource = _AudioSource[(int)Define.Sound.Effect];
            audioSource.pitch = Pitch;
            audioSource.PlayOneShot(audio);
        }
    }
    public void Clear()
    {
        foreach (AudioSource audiosource in _AudioSource)
        {
            audiosource.clip = null;
            audiosource.Stop();
        }
        _audioClip.Clear();
    }

    AudioClip GetOrAddAudioClip(string Path, Define.Sound type = Define.Sound.Effect)
    {
        if (Path.Contains("Sounds/") == false)
        {
            Path = $"Sounds/{Path}";
        }

        AudioClip audioClip = null;

        if (type == Define.Sound.BGM)
        {
            AudioClip AudioClip = GameManager.Resource.Load<AudioClip>(Path);
        }
        else
        {
            if (_audioClip.TryGetValue(Path, out audioClip) == false)
            {
                audioClip = GameManager.Resource.Load<AudioClip>(Path);
                _audioClip.Add(Path, audioClip);
            }
        }
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {Path}");
        }
        return audioClip;
    }
}

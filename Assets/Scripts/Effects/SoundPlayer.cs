using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
{
    private static SoundPlayer _instance;
    private static SoundPlayer Instance => _instance ?? (_instance = new SoundPlayer(10));


    private readonly Dictionary<string, AudioClip> _sounds;
    private readonly AudioSource [] _sources;
    private int currentChannel = 0;

    private SoundPlayer(int sourcesCount) 
    {
        _sounds = new Dictionary<string, AudioClip>();
        var sourcesGameObject = new GameObject("SoundSources");
        
        _sources = new AudioSource[sourcesCount];            
        for (int i = 0; i < sourcesCount; i++)
        {
            _sources[i] = sourcesGameObject.AddComponent<AudioSource>();                 
        }
        
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");
        foreach(AudioClip clip in clips)
        {
            _sounds.Add(clip.name, clip);
        }            
    }

    private void PlaySound(string name, float volume = 1)
    {        
        _sources[currentChannel].clip = _sounds[GetRandomSound(name)];
        _sources[currentChannel].pitch = 1 + Random.Range(-0.3f, 0.3f);
        _sources[currentChannel].volume = volume;
        _sources[currentChannel].Play();                
        currentChannel = (currentChannel + 1) % _sources.Length;
    }

    public static void Play(string name, float volume = 1)
    {
        Instance.PlaySound(name,volume);
    }

    private string GetRandomSound(string name)
    {
        List<string> clips = new List<string>();
        foreach (KeyValuePair<string, AudioClip> pair in _sounds)
        {
            if (pair.Key.Contains(name))
            {
                clips.Add(pair.Key);
            }
        }
        return clips[(int)(UnityEngine.Random.value * clips.Count)];
    }

}


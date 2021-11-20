using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public AudioMixerGroup audioMixerGroup;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
        instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroup;
            Debug.Log(s.source.volume);
        }
    }

    void Start()
    {
        Play("PlaceHolderBackground");
    }

    // Update is called once per frame
    public static void Play(string name)
    {
        if (Board.original||instance==null) return;
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s==null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if(s.source!=null)
        s.source.Play();
    }
}

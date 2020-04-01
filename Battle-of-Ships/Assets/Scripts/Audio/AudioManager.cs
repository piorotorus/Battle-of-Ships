using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    public static AudioManager audioManagerInstance;
    private AudioSource audioSource;
    void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    [ContextMenu("AddAudioToList")]
    void AddAudioToList()
    {
        sounds.Add(null);
    }

    public void PlaySound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s != null)
        {
            audioSource.clip = s.clip;
            audioSource.volume = s.volume;
            audioSource.loop = s.loop;
            audioSource.outputAudioMixerGroup = s.mixerGroup;
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            Debug.LogError("Can't find " + name + " clip");
        }
    }
    
    public void PlaySound(string name,AudioSource audioSource)
    {
        Sound s=  sounds.Find(sound => sound.name == name);
        if (s != null)
        {
            audioSource.clip = s.clip;
            audioSource.loop = s.loop;
            audioSource.volume = s.volume;
            audioSource.outputAudioMixerGroup = s.mixerGroup;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Can't find "+name+" clip");
        }
    }
}

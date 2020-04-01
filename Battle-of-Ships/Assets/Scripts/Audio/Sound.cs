﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="New Sound", menuName="Sound")]
public class Sound : ScriptableObject
{

    public new string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public AudioMixerGroup mixerGroup;
    public bool playInAudioManager;
    [HideInInspector]
    public AudioSource source;
}
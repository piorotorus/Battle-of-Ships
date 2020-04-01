using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    private const string MUSIC_NAME = "Music";
    void Start()
    {
       audioSource=GetComponent<AudioSource>();
        AudioManager.audioManagerInstance.PlaySound(MUSIC_NAME,audioSource);
    }
}

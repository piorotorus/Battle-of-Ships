using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private const string MUSIC_NAME = "WaterBackground";
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        AudioManager.audioManagerInstance.PlaySound(MUSIC_NAME,audioSource);
    }
}

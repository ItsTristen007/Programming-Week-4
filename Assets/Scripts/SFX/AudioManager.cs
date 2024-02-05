using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioClip coinGet;
    [SerializeField] private AudioClip winSound;

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
}
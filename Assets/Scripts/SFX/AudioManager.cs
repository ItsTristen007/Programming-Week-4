using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Sets up the sounds 
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioClip coinGet;
    [SerializeField] private AudioClip winSound;

    // when called play the sound 
    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
}
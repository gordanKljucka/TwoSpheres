using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audio;
    public AudioClip mySound;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    static void PlaySound()
    {
       // audio.PlayOneShot(mySound);   //  static radi probleme
    }
    
}

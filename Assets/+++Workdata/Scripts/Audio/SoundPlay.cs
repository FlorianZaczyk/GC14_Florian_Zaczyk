using System;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
 
    public AudioSource Audio;
   

    public void PlaySound()
    {
        Audio.Play();
    }
 
}

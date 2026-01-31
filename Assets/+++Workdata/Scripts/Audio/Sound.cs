using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;

    void OnEnable()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
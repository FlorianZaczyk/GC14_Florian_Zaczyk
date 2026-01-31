using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    public AudioSource gameOverAudio;

    void OnEnable()
    {
        if (gameOverAudio != null)
        {
            gameOverAudio.Play();
        }
    }
}
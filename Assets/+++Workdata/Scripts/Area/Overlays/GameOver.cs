using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    
    public GameObject gameOverContainer;
    private bool _isPaused;
    public GameObject player;
    public AudioSource gameMusic;
    private PlayerController _playerController;
    

    private void gameOverScreen()
    {
        _isPaused = !_isPaused;
        gameOverContainer.SetActive(true);
        Time.timeScale = _isPaused ? 0 : 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameMusic.Pause();
    }

    public void RestartScene(int index)
    {
        SceneManager.LoadScene(index);
        gameOverContainer.SetActive(false);
        Time.timeScale = 1;
    }

}

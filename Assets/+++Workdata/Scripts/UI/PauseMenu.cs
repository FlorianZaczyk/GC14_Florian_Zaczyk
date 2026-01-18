using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pauseMenuContainer;

    private bool _isPaused;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
            {
                OpenPauseMenu(); 
            }

        }
    }

    private void OpenPauseMenu()
    {
        _isPaused = !_isPaused;
        pauseMenuContainer.SetActive(true);
        Time.timeScale = _isPaused ? 0 : 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        pauseMenuContainer.SetActive(false);
    }
}
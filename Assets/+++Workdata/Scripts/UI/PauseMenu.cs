using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseMenuContainer;
    private PlayerController _playerController;

    private bool _isPaused;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _isPaused = false;
    }

    public void OpenPauseMenu()
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
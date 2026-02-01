
using System;
using UnityEngine;

public class MainMenu_UiManager : MonoBehaviour
{
    public GameObject winningContainer;
    public GameObject mainMenuContainer;
    public GameObject loadMenuContainer;
    public GameObject optionsMenuContainer;
    public GameObject creditsMenuContainer;
    public GameObject pauseMenuContainer;
    public GameObject audioOptionsMenuContainer;
    public GameObject gameOverScreenContainer;
    public GameObject _currentMenu;
    
    public bool soundOn = true;
    public AudioSource uiAudioSource;


    private void Awake()
    {
        _currentMenu = mainMenuContainer;
        soundOn = false;
       // uiAudioSource.mute = soundOn;
    }

    
    public void OpenLoadMenu()
    {
        _currentMenu.SetActive(false);
        
        loadMenuContainer.SetActive(true);
        _currentMenu = loadMenuContainer;
    }

    public void OpenMainMenu()
    {
        _currentMenu.SetActive(false);
        
        mainMenuContainer.SetActive(true);
        _currentMenu = mainMenuContainer;
    }
    
    public void OpenOptionsMenu()
    {
        _currentMenu.SetActive(false);
        
        optionsMenuContainer.SetActive(true);
        _currentMenu = optionsMenuContainer;
    }
    
    public void OpenCreditsMenu()
    {
        _currentMenu.SetActive(false);
        
        creditsMenuContainer.SetActive(true);
        _currentMenu = creditsMenuContainer;
    }
    public void OpenAudioOptionsMenu()
    {
        _currentMenu.SetActive(false);
        
        audioOptionsMenuContainer.SetActive(true);
        _currentMenu = audioOptionsMenuContainer;
    }
    
    public void OpenPauseMenu()
    {
        _currentMenu.SetActive(false);
        
        pauseMenuContainer.SetActive(true);
        _currentMenu = pauseMenuContainer;
    }
    
    public void OpenGameOverScreen()
    {
        _currentMenu.SetActive(false);
        
        gameOverScreenContainer.SetActive(true);
        _currentMenu = gameOverScreenContainer;
    }
    
    public void OpenWinningScreen()
    {
        _currentMenu.SetActive(false);
        
        winningContainer.SetActive(true);
        _currentMenu = winningContainer;
    }
    
    public void ToggleSound()
    {
        soundOn = !soundOn;
        uiAudioSource.mute = soundOn;
    }
}
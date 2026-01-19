
using UnityEngine;

public class MainMenu_ButtonManager : MonoBehaviour
{
    public MainMenu_UiManager uiManager;
    public SceneLoaderManager sceneLoaderManager;
    public void Button_OpenLoadMenu()
    {
        uiManager.OpenLoadMenu();
    }

    public void Button_OpenMainMenu()
    {
        uiManager.OpenMainMenu();
    }

    public void Button_OpenOptionsMenu()
    {
        uiManager.OpenOptionsMenu();
    }

    public void OpenAudioOptionsMenu()
    {
       uiManager.OpenAudioOptionsMenu();
    }
    
    public void Button_OpenPauseMenu()
    {
        uiManager.OpenPauseMenu();
    }
    
    public void OpenGameOverScreen()
    {
        uiManager.OpenGameOverScreen();
    }
    
    public void Button_LoadSceneByName(string sceneName)
    {
        sceneLoaderManager.LoadScene(sceneName);
    }

    public void Button_LoadSceneByIndex(int index)
    {
        sceneLoaderManager.LoadScene(index);

    }

    public void Button_ToggleSound()
    {
        uiManager.ToggleSound();
    }
}
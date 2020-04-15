using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject audioSettings;
    public GameObject mainMenu;
    public bool changer;
    public GameObject backButton;



    public void settingsMenuDisabler()
    {
        if (mainMenu.activeSelf == false)
        {
            audioSettings.SetActive(false);
            backButton.SetActive(false);
           mainMenu.SetActive(true);
        }
        else
        {
            audioSettings.SetActive(true);
            backButton.SetActive(true);
            mainMenu.SetActive(false);
        }

    }
    public void AudioSettings()
    {
        if(mainMenu.activeSelf == false && audioSettings.activeSelf == false)
        {
            mainMenu.SetActive(true);
        }
        if (audioSettings.activeSelf == true)
        {
            mainMenu.SetActive(false);
        }
    }
    public void onClick()
    {
        if (audioSettings.activeSelf == false)
        {

            mainMenu.SetActive(false);
            audioSettings.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            audioSettings.SetActive(false);
        }


    }
    public void Restart()
    {
      
        SceneManager.LoadScene(GameStatus.status.currentLevel);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("UIMainmenukehitysScene");
    }
}

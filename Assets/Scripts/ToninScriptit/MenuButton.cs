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


    void Start()
    {
       
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 1)
        {

            mainMenu = GameObject.Find("/Misc stuff/Canvas/Mainmenu");
            backButton = GameObject.Find("/Misc stuff/Canvas/SoundSettingsPanel/AudioSettingsPanel/BacktoMainMenuFromAudio");
            audioSettings = GameObject.Find("/Misc stuff/Canvas/SoundSettingsPanel");

        }
    }

    public void AudioMainMenu()
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
    public void SettingsMenuDisabler()
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
    public void SettingsMenuDisablerInGame()
    {
        if (mainMenu.activeSelf == false)
        {
            audioSettings.SetActive(false);
         
            mainMenu.SetActive(true);
        }
        else
        {
            audioSettings.SetActive(true);
           
            mainMenu.SetActive(false);
        }

    }
    public void AudioSettings()
    {
        if (mainMenu.activeSelf == false && audioSettings.activeSelf == false)
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
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        //GameObject levelController = GameObject.Find("LevelController");
       
        Scene scene = SceneManager.GetActiveScene();
        int sceneToLoad = scene.buildIndex;
        GameStatus.status.DeathRestart(sceneToLoad);
    }
    public void UnLoad()
    {
        GameStatus.status.UnLoad();
        SettingsMenuDisabler();
    }
    public void BackToMenu()
    {
        GameStatus.status.currentLevel = "UIMainmenukehitysScene";
        SceneManager.LoadScene("UIMainmenukehitysScene");
        AudioImmortality.immortal.ChangeBackgroundMusic("UIMainmenukehitysScene");
    }
}

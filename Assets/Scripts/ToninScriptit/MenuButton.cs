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
    private GameObject masterSyringe;
    private GameObject musicSyringe;
    private GameObject effectsSyringe;

    void Start()
    {
       
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 1)
        {


            mainMenu = GameObject.Find("/Misc stuff/Canvas/Mainmenu");
            backButton = GameObject.Find("/Misc stuff/Canvas/BacktoMainMenuFromAudio");
            audioSettings = GameObject.Find("/Misc stuff/Canvas/SoundSettingsPanel");

        }
        if (scene.buildIndex == 1 )
        {
            masterSyringe = GameObject.Find("/Misc stuff/Canvas/AudioSettingsPanel/BaseImage/MasterVolumeSlider");
            musicSyringe = GameObject.Find("/Misc stuff/Canvas/AudioSettingsPanel/BaseImage/MusicVolumeSlider");
            effectsSyringe = GameObject.Find("/Misc stuff/Canvas/AudioSettingsPanel/BaseImage/EffectVolumeSlider");
        }
    }

    public void AudioMainMenu()
    {
        if (mainMenu.activeSelf == false)
        {
            audioSettings.SetActive(false);
            backButton.SetActive(false);
            mainMenu.SetActive(true);
            masterSyringe.SetActive(false);
            musicSyringe.SetActive(false);
            effectsSyringe.SetActive(false);
        }
        else
        {
            masterSyringe.SetActive(true);
            musicSyringe.SetActive(true);
            effectsSyringe.SetActive(true);

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
        GameObject levelController = GameObject.Find("LevelController");
        GameStatus.status.DeathRestart(levelController.GetComponent<LevelController>().currentFloor);

    }
    public void UnLoad()
    {
        GameStatus.status.UnLoad();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("UIMainmenukehitysScene");
        AudioImmortality.immortal.ChangeBackgroundMusic("UIMainmenukehitysScene");
    }
}

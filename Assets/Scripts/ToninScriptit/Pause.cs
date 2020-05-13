using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private GameObject PausemenuPanel;
    private GameObject menu;
    public bool paused;



    private GameObject audioSettings;

    public bool alive;
    void Start()
    {
        PausemenuPanel = GameObject.Find("/Misc stuff/Canvas/PauseMenu");
        menu = GameObject.Find("/Misc stuff/Canvas/Mainmenu");
        audioSettings = GameObject.Find("/Misc stuff/Canvas/SoundSettingsPanel");

    }

    void Awake()
    {
        paused = false;

        alive = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true || Input.GetKeyDown(KeyCode.P) == true)
        {
            paused = !paused;
            Pauser();
        }
        var deathPanel = GameObject.Find("/Misc stuff/Canvas/DeathPanel");
        if (deathPanel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartFromSpace();

            }
        }

    }

    public void Unpause()
    {
        paused = false;
        Pauser();
    }

    public void Pauser()
    {

        if (paused && alive == true)
        {
            //Cursor.visible = true;

            Time.timeScale = 0.00001F;
            PausemenuPanel.SetActive(true);

            if (menu.activeSelf == true || audioSettings.activeSelf == true)
            {
                return;
            }
            if (menu.activeSelf == false && audioSettings.activeSelf == false)
            {
                menu.SetActive(true);
            }

        }
        if (!paused && alive == true)
        {

            PausemenuPanel.SetActive(false);
            //Cursor.visible = false;
            menu.SetActive(false);
            audioSettings.SetActive(false);

            Time.timeScale = 1;

        }
        if (alive != true)
        {

            Cursor.visible = true;
            // GameObject soundButton = GameObject.FindWithTag("soundsettings");
            Time.timeScale = 0.00001F;



        }
    }
    public void RestartFromSpace()
    {
        SceneManager.LoadScene(GameStatus.status.currentLevel);
    }

}

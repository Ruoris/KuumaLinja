using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
   
    private GameObject menu;
    public bool paused;
    private GameObject backButton;
    private GameObject audioSettings;
    private GameObject player;
    public bool alive;
    void Start()
    {
        menu = GameObject.Find("/Misc stuff/Canvas/Mainmenu");
        backButton= GameObject.Find("/Misc stuff/Canvas/BacktoMainMenuFromAudio");
        audioSettings = GameObject.Find("/Misc stuff/Canvas/SoundSettingsPanel");
        player = GameObject.Find("Player");
    }

    void Awake()
    {
        paused = false;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Pauser();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartFromSpace();
        }
    }

    public void Unpause()
    {
            paused = false;
    } 
    
    public void Pauser()
    {
        if (Input.GetKeyDown(KeyCode.Escape)==true || Input.GetKeyDown(KeyCode.P) == true)
        {
            paused = !paused;

        }
        if (paused&&alive == true)
        {
            Cursor.visible = true;
           // GameObject soundButton = GameObject.FindWithTag("soundsettings");
            Time.timeScale = 0.00001F;

            if (menu.activeSelf == true ||audioSettings.activeSelf==true  )
            {
                return;
            }
            if (menu.activeSelf == false && audioSettings.activeSelf == false)
            {
                menu.SetActive(true);
            }

        }
        if (!paused&&alive==true)
        {
            Cursor.visible = false;
            menu.SetActive(false);
            audioSettings.SetActive(false);
            backButton.SetActive(false);
            Time.timeScale = 1;
        }
        if(alive != true)
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
   
    public GameObject menu;
    public bool paused;
    public GameObject backButton;
    public GameObject audioSettings;



    void Awake()
    {
        paused = false;

    }

    // Update is called once per frame
    void Update()
    {
        Pauser();
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
        if (paused)
        {
           // GameObject soundButton = GameObject.FindWithTag("soundsettings");
            Time.timeScale = 0.0001F;

            if (menu.activeSelf == true ||audioSettings.activeSelf==true  )
            {
                return;
            }
            if (menu.activeSelf == false && audioSettings.activeSelf == false)
            {
                menu.SetActive(true);
            }

        }
        if (!paused)
        {
             menu.SetActive(false);
            audioSettings.SetActive(false);
            backButton.SetActive(false);
            Time.timeScale = 1;
         
        }
    }
}

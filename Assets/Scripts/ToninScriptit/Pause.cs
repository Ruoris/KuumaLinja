using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pause : MonoBehaviour
{
   
    public GameObject menu;
    public bool paused;
    public GameObject backButton;
    public GameObject audioSettings;
    public GameObject DT1, DT2, DT3, DT4, DT5, DT6, DT7; // dialogue trigger


    void Awake()
    {
        paused = false;
        DT1 = GameObject.Find("DT1");
        DT2 = GameObject.Find("DT2");
        DT3 = GameObject.Find("DT3");
        DT4 = GameObject.Find("DT4");
        DT5 = GameObject.Find("DT5");
        DT6 = GameObject.Find("DT6");
        DT7 = GameObject.Find("DT7");

    }

    // Update is called once per frame
    void Update()
    {
        if (DT1 != null && DT2 != null && DT3 != null && DT4 != null && DT5 != null && DT6 != null && DT7 != null)
        {
            if (DT1.GetComponent<DialogueScript>().dialogue == false && DT2.GetComponent<DialogueScript>().dialogue == false && DT3.GetComponent<DialogueScript>().dialogue == false && 
                DT4.GetComponent<DialogueScript>().dialogue == false && DT5.GetComponent<DialogueScript>().dialogue == false && DT6.GetComponent<DialogueScript>().dialogue == false && 
                DT7.GetComponent<DialogueScript>().dialogue == false)
            {
                Pauser();
            }
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
            // menu.SetActive(false);
            //audioSettings.SetActive(false);
            //backButton.SetActive(false);
            //Time.timeScale = 1;
        }
    }
}

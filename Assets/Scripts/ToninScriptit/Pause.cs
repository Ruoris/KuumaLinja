using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
   
    public GameObject menu;
    public bool paused;
    public GameObject backButton;
    public GameObject audioSettings;
    public GameObject dialogueTrigger, dialogueTrigger2, dialogueTrigger3, dialogueTrigger4;


    void Awake()
    {
        paused = false;
        dialogueTrigger = GameObject.Find("DialogueTrigger1");
        dialogueTrigger2 = GameObject.Find("DialogueTrigger2");
        dialogueTrigger3 = GameObject.Find("DialogueTrigger3");
        dialogueTrigger4 = GameObject.Find("DialogueTrigger4");
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueTrigger.GetComponent<DialogueScript>().dialogue == false && dialogueTrigger2.GetComponent<DialogueScript>().dialogue == false && dialogueTrigger3.GetComponent<DialogueScript>().dialogue == false && dialogueTrigger4.GetComponent<DialogueScript>().dialogue == false)
        {
            Pauser();
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
             menu.SetActive(false);
            audioSettings.SetActive(false);
            backButton.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

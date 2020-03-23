using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void BackButtonActivator()
    {

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
}

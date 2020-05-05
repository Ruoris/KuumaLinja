using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public bool dialoguePause;
    private int index;
    public float typingSpeed;
    public GameObject continueButtuon;
    public AudioSource textSound;

    void Start()
    {
        dialoguePause = true;
        StartCoroutine(Type());
    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButtuon.SetActive(true);
            textSound.Stop();
            if (Input.GetKeyDown("space"))
            {
                NextSentence();
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialoguePause = true;
            Time.timeScale = 0.00001f;
            textDisplay.text += letter;
            textSound.Play();
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    public void NextSentence()
    {
        continueButtuon.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            dialoguePause = false;
            Time.timeScale = 1f;
            textDisplay.text = "";
            continueButtuon.SetActive(false);

            if (SceneManager.GetActiveScene().name == "Cutscene")
            {
                Debug.Log("Loading level 1");
                SceneManager.LoadScene("Level 1.2");
            }
        }  
    }
}

/*
World has fallen into chaos. A new drug capable of mutating humans into beasts has gone viral and
now instead of ingesting the drug, the effects of it can be transferred from human to human with just
touch. You are an agent tasked with infiltrating a skyscraper to stop the organization
believed to be behind this.

It is already dark outside as you step inside the building. 
Not knowing what lies ahead, you start the investigation.


You can hear people going about beyond the walls...
*/
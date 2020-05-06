using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO.Pipes;

public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public bool dialoguePause;
    private int index;
    public float typingSpeed;
    public GameObject continueButtuon;
    public AudioSource textSound;
    public GameObject character1, character2;
    public bool npc = false;

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
            if(letter == '-')
            {
                npc = true;
                Debug.Log("character 2 active");
                character1.SetActive(false);
                character2.SetActive(true);
            }
            else if (!npc)
            {
                character1.SetActive(true);
                character2.SetActive(false);
            }

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
            npc = false;
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

/*
Phew. It's pouring out there, at least here I can dry myself off.
This should be a good place to start based on the information I have.
...
...
Such a fancy place. I wonder if they even care about what's going on outside.
I need to hurry and get to the bottom of this, by force if necessary.

---------------
NPC: Good evening. How can I gelp?
I want to meet your boss.
NPC: The manager is at a meeting right now. Please take a seat while waiting for them to finish.
NPC: It shouldn't take more than a couple of hours tops.
NPC: So please take a seat, they will be with you when ready.

--------------------
Hmmm... An open door.
I really don't have time for this nonsense.
Time to take matters to my own hands.

--------------------
NPC: You can't come in here! It's employees only!
I come wherever I please.
NPC: No! Guards! Help!
 */

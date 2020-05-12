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
    public GameObject character1, character2, character3;
    public bool npc = false;
    public int dialogues;

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
                character3.SetActive(false);
            }

            else if (letter == '~')
            {
                npc = true;
                Debug.Log("character 3 active");
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(true);
            }

            else if (!npc)
            {
                character1.SetActive(true);
                character2.SetActive(false);
                character3.SetActive(false);
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

            if (SceneManager.GetActiveScene().name == "Cutscene 1")
            {
                Debug.Log("Loading level 2");
                //SceneManager.LoadScene("Level2");
                GameStatus.status.LevelEnd();
            }

            else if (SceneManager.GetActiveScene().name == "Cutscene 2")
            {
                Debug.Log("Loading level 3");
                //SceneManager.LoadScene("Level3");
                GameStatus.status.LevelEnd();
            }
        }  
    }
}

/*
World has fallen into chaos. A new drug capable of mutating humans into beasts has gone viral and
now instead of ingesting the drug, the effects of it can be transferred from human to human with just
touch. 

You are an agent tasked with investigating the matter to stop the organization
believed to be behind this.

It is dark and rainy outside as you step inside the building. 

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
I walk wherever I please.
NPC: No! Guards! Help!

    --------------
~Help! In here!
???

    --------------
Hello? Someone in here?
~Thank God you're here!
~They captured me to stop me from giving information to the public!
What kind of information?
~The organization I work for went mad and started making monsters out of ordinary people.
~Me and some of my colleagues tried to stop this but they forced us to make even stronger drugs for them.
~I... I couldn't bear it... They had gone too far.
~I tried to escape the company grounds but couldn't get far before they caught me.
~Maybe you could help the others?
Where exactly is all this taking place?
~In the big building, you should be able to see it from here.
~Please help the others. 
I doubt I can make it without help.
~It's dangerous to go alone, take this.
What a strange thing to say.
But anyhow, I will take care of this.
 */

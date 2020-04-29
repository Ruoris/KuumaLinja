using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Text : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButtuon;
    public AudioSource audioclip;
    void Start()
    {
        StartCoroutine(Type());
    }
    void Update()
    {
        if(textDisplay.text== sentences[index])
        {
            continueButtuon.SetActive(true);
            audioclip.Stop();
            if (Input.GetButton("Jump"))
            {
                NextSentence();
            }
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            audioclip.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
        continueButtuon.SetActive(false);
        if(index< sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {          
            textDisplay.text = "";
            continueButtuon.SetActive(false);

        }
        
    }
 
}

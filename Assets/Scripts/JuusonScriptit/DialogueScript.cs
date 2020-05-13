using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueTrigger;
    public GameObject dialogue1;
    public bool dialogue;
    public GameObject dialoguePause;

    // Start is called before the first frame update
    public void Start()
    {
        dialogue = false;

    }

    // Update is called once per frame
    public void Update()
    {
        dialoguePause = GameObject.Find("TextManager");
        
        if (dialoguePause != null && dialoguePause.GetComponent<TextScript>().dialoguePause == false)
        {
            dialogue = false;
            dialogue1.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogue = true;
            dialogue1.SetActive(true);
        }
    }
}

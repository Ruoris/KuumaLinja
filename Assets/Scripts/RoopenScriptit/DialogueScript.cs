using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueTrigger;
    public GameObject dialogue1;
    public bool dialogue;
    public GameObject dialoguePause;
    public Scene scene;
    // Start is called before the first frame update
    public void Start()
    {
        dialogue = false;
        scene = SceneManager.GetActiveScene();

        
    }

    // Update is called once per frame
    public void Update()
    {
        dialoguePause = GameObject.Find("TextManager");
        
        if (dialoguePause != null && dialoguePause.GetComponent<TextScript>().dialoguePause == false)
        {
            dialogue = false;
            dialogue1.SetActive(false);
            AutoSkip();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            dialoguePause.GetComponent<TextScript>().Skip();
            dialogue = false;
            dialogue1.SetActive(false);
            AutoSkip();
          
        }
    }
    private void AutoSkip()
    {
        if (scene.buildIndex == 3)
        {
            if (this.gameObject.tag == "DT1")
            {
                GameStatus.status.Level1DT1 = true;

            }
            if (this.gameObject.tag == "DT2")
            {
                GameStatus.status.Level1DT2 = true;

            }
            if (this.gameObject.tag == "DT3")
            {
                GameStatus.status.Level1DT3 = true;

            }
            if (this.gameObject.tag == "DT4")
            {
                GameStatus.status.Level1DT4 = true;

            }
            if (this.gameObject.tag == "DT5")
            {
                GameStatus.status.Level1DT5 = true;

            }
            if (this.gameObject.tag == "DT6")
            {
                GameStatus.status.Level1DT6 = true;

            }
            if (this.gameObject.tag == "DT7")
            {
                GameStatus.status.Level1DT7 = true;

            }

        }
        if (scene.buildIndex == 6)
        {
            if (this.gameObject.tag == "DT1")
            {
                GameStatus.status.Level6DT1 = true;

            }
            if (this.gameObject.tag == "DT2")
            {
                GameStatus.status.Level6DT2 = true;

            }
            if (this.gameObject.tag == "DT3")
            {
                GameStatus.status.Level6DT3 = true;

            }
            if (this.gameObject.tag == "DT4")
            {
                GameStatus.status.Level6DT4 = true;

            }
            if (this.gameObject.tag == "DT5")
            {
                GameStatus.status.Level6DT5 = true;

            }
            if (this.gameObject.tag == "DT6")
            {
                GameStatus.status.Level6DT6 = true;

            }
            if (this.gameObject.tag == "DT7")
            {
                GameStatus.status.Level6DT7 = true;

            }

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
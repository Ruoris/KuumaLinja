using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;

    public GameObject openUp, openDown;

    void Start()
    {
        var doorAnimation = GetComponent<Animator>();
        var doorSound = GetComponent<AudioSource>();
        doorAnimation.enabled = false;
        doorSound.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var doorAnimation = GetComponent<Animator>();
        var doorSound = GetComponent<AudioSource>();

        if (other.gameObject.tag == "Player" && gameObject.name == "OpenUp")
        {
            doorAnimation.enabled = true;
            doorSound.enabled = true;
            openDown.SetActive(false);
        }


        else if (other.gameObject.tag == "Player" && gameObject.name == "OpenDown")
        {
            doorAnimation.enabled = true;
            doorSound.enabled = true;
            openUp.SetActive(false);
        }

        else if (other.gameObject.tag == "Player" && gameObject.name == "DoorLeft")
        {
            doorAnimation.enabled = true;
<<<<<<< HEAD
            doorSound.enabled = true;
=======
>>>>>>> origin/TuomaksenBranch2
        }

        else if (other.gameObject.tag == "Player" && gameObject.name == "DoorRight")
        {
            doorAnimation.enabled = true;
<<<<<<< HEAD
            doorSound.enabled = true;
=======
>>>>>>> origin/TuomaksenBranch2
        }
    }
}
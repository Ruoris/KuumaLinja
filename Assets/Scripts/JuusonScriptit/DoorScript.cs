using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;

    public GameObject upSide, downSide;

    void Start()
    {
        var doorAnimation = GetComponent<Animator>();
        doorAnimation.enabled = false;
    }
        private void OnCollisionEnter2D(Collision2D other)
    {
            var doorAnimation = GetComponent<Animator>();
            if (other.gameObject.name == "Player" && gameObject.name == "DoorDown")
            {
                doorAnimation.enabled = true;
                upSide.SetActive(false);
            }

            else if (other.gameObject.name == "Player" && gameObject.name == "DoorUp")
            {
                doorAnimation.enabled = true;
                downSide.SetActive(false);
            }
    }
}
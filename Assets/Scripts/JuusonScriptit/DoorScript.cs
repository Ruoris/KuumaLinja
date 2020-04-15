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
        //if(Time.time > 1)
        //{
            var doorAnimation = GetComponent<Animator>();
            if (other.gameObject.name == "Player" && gameObject.name == "DoorDown")
            {
                upSide.SetActive(false);

                doorAnimation.enabled = true;
            }

            else if (other.gameObject.name == "Player" && gameObject.name == "DoorUp")
            {
                downSide.SetActive(false);
                doorAnimation.enabled = true;
            }
        //}
    }
}
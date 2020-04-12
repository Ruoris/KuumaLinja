using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player" /*&& !gameObject.CompareTag("Door")*/)
        {
            var doorAnimation = GetComponent<Animator>();
            doorAnimation.enabled = true;
        }
    }
}
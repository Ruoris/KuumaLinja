using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnable : MonoBehaviour
{
    public bool canShoot; 
    void Start()
    {
        canShoot = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            canShoot = false;
        }
    }
    void OnTriggerExit2D(Collider2D exiter)
    {
        if (exiter.gameObject.tag == "Wall")
        {
            canShoot = true;
        }

    }


}

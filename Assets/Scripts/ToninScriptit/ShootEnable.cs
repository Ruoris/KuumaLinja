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
        if (other.gameObject.tag == "Wall" && other.gameObject.name=="Wall 3(Clone)" 
            || other.gameObject.name == "WallObjectSideways(Clone)")
        {
            canShoot = false;
        }
    }
    void OnTriggerExit2D(Collider2D exiter)
    {
        if (exiter.gameObject.tag == "Wall" && exiter.gameObject.name == "Wall 3(Clone)" 
            || exiter.gameObject.name == "WallObjectSideways(Clone)")
        {
            canShoot = true;
        }

    }


}

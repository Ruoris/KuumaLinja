using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public int bulletsLeft;
    public int ammoCapacity;
    private bool started;
    public float timer;

    public void Start()
    {
        timer = 0;
        started = true;
        //bulletsLeft = ammoCapacity;
    }
    public void Update()

    {
        if (started)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                gameObject.layer = 11;
                started = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{
    public GameObject[] pistolBullets;


    public void Start()
    {

    }
    public void Update()
    {
        pistolBullets = GameObject.FindGameObjectsWithTag("PistolBulletImage");
    }
    public void ChangeColor(int bulletsLeft)
    {

        Color used = new Color32(75, 75, 75, 255);
        pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
    }

    public void ReturnColor(int ammoCapacity)
    {
        for (int i = 0; i < ammoCapacity; i++)
        {
            Color used = new Color32(255, 255, 255, 255);

            pistolBullets[i].GetComponent<Image>().color = used;
        }
    }

    public void PartialColorToUsed(int ammoCapacity, int ammoleft)
    {
        while (ammoleft < ammoCapacity)
        {
            Color used = new Color32(75, 75, 75, 255);

            pistolBullets[ammoleft].GetComponent<Image>().color = used;
            ammoleft++;
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{
    public GameObject[] pistolBullets;
    public GameObject player;

    public void Update()
    {
        pistolBullets = GameObject.FindGameObjectsWithTag("PistolBulletImage");
    }

    public void ChangeColor()
    {

        int bulletsLeft = player.GetComponent<Weapons>().ammoLeft;

        Color used = new Color32(75, 75, 75, 255);
        pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
    }

    public void ReturnColor(int ammocapacity)
    {
        for (int i = 0; i < ammocapacity; i++)
        {
            Color used = new Color32(255, 255, 255, 255);

            pistolBullets[i].GetComponent<Image>().color = used;
        }
    }

    public void PartialColorToUsed(int ammoCapacity, int ammoleft)
    {
        while(ammoleft < ammoCapacity)
        {
            Color used = new Color32(75, 75, 75, 255);

            pistolBullets[ammoleft].GetComponent<Image>().color = used;
            ammoleft++;
        }
    }
}

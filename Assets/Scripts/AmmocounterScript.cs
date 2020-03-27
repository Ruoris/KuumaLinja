using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{

    public GameObject[] pistolBullets;
    public GameObject pistolbullet;
    public int bulletsLeft=4;
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeColor();
        }
    }
    void Start()
    {
       
    }
    public void GetPistolBullets()
    {

    }


  public void ChangeColor()
    {
        Color used = new Color32(75, 75, 75, 255);
        //pistolbullet.GetComponent<Image>().color = used;
        pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
        bulletsLeft--;


    }
}

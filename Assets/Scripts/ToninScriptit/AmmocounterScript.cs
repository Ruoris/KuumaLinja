using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{

    public GameObject[] pistolBullets;
    public GameObject Player;
    public int bulletsLeft=9;
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ChangeColor();
        } 
        bulletsLeft = Player.GetComponent<Weapons>().ammoLeft -1;
    }
    void Start()
    {
      


    }
    public void GetPistolBullets(int ammoCapacity )
    {

    }


  public void ChangeColor()
    {
        Color used = new Color32(75, 75, 75, 255);
       
        pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
        bulletsLeft--;


    }
}

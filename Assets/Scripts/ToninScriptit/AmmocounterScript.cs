using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{

    public GameObject[] pistolBullets;
    public GameObject Player;
    private int bulletsLeft;
    void Update()
    { bulletsLeft = Player.GetComponent<Weapons>().ammoLeft;
        if (Input.GetButton("Fire1"))
        {
            if (bulletsLeft != 0)
            {
 ChangeColor();
            }
           
        } 
       
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
       


    }
    public void ReturnColor()
    {
        for(int i = 0; i < 10; i++)
        {
            Color used = new Color32(255, 255, 255, 255);

            pistolBullets[i].GetComponent<Image>().color = used;
        }
    }
}

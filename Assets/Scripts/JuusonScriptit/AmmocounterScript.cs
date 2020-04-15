using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{
        public GameObject[] pistolBullets;
        public GameObject Player;
        private int bulletsleft;

       void update()
       {



            if (Input.GetButton("fire1"))
            {
                changecolor();
  

  
            }
       }

 
       public void Getpistolbullets(int  ammoCapacity)
       {

            //GameObject pistolBullets[ ammoCapacity];

       }


       public void changecolor()
       {

        int bulletsLeft = Player.GetComponent<Weapons>().ammoLeft;

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
}

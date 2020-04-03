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
    {



        if (Input.GetButton("Fire1"))
        {
                ChangeColor();
<<<<<<< HEAD

=======
>>>>>>> JuusonBranch2
        }
    }

    void Start()
    {



    }
    public void GetPistolBullets(int ammoCapacity)
    {
<<<<<<< HEAD
        //GameObject pistolBullets[ammoCapacity];
=======
>>>>>>> JuusonBranch2
    }


    public void ChangeColor()
    {
<<<<<<< HEAD
        //bool emptyMagazine = Player.GetComponent<Weapons>().emptyMagazine;
        int bulletsLeft = Player.GetComponent<Weapons>().ammoLeft;

        //if (emptyMagazine)
        //{
        //    ReturnColor();
        //}

            Color used = new Color32(75, 75, 75, 255);
            pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
=======
        int bulletsLeft = Player.GetComponent<Weapons>().ammoLeft;

        Color used = new Color32(75, 75, 75, 255);
        pistolBullets[bulletsLeft].GetComponent<Image>().color = used;
>>>>>>> JuusonBranch2
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

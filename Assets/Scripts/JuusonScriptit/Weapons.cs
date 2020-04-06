using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject pistol, shotgun, assaultRifle, machineGun, flameThrower, grenade;
    public GameObject player, playerMelee, playerOneHand, playerBothHands;
    public int bulletForce;
    public float fireRate;
    public bool emptyMagazine;


    //public AudioClip equipClip;

    //public AudioSource equipClip;


    public GameObject uiAmmoCounter;

    public int equippedGun, ammoCapacity, ammoLeft;
    

    // Start is called before the first frame update
    void Start()
    {
        emptyMagazine = true;
        ammoLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EquipGun();

        int shotsFired = GetComponent<Pshoot>().shotsFired;

        ammoLeft = ammoCapacity - shotsFired;
        if(ammoLeft <= 0)
        {
            equippedGun = 0;
        }

    }

    void EquipGun()
    {
        uiAmmoCounter.SetActive(true);

        bulletForce = 40;


        foreach (Transform weapon in player.GetComponentsInChildren<Transform>())
        {
            for (int i = 0; i < player.transform.childCount; i++)
            {
                // deactivates other weapons
                var child = player.transform.GetChild(i).gameObject;

                if (child != null && child.name.Contains("Weapon") || child.name.Contains("Hand"))
                {
                    child.SetActive(false);
                }
            }

            switch (equippedGun)
            {
                // gun selection
                case 0:
                    uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
                    playerBothHands.SetActive(false);
                    playerOneHand.SetActive(false);
                    playerMelee.SetActive(false);
                    uiAmmoCounter.SetActive(false);
                    emptyMagazine = true;
                    ammoCapacity = 0;

                    fireRate = 0f;

                    break;
                case 1:
                    pistol.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 10;
                    fireRate = 0.3f;
                    break;
                case 2:
                    shotgun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 5;
                    fireRate = 1;
                    break;
                case 3:
                    assaultRifle.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 30;
                    fireRate = 0.1f;
                    break;
                case 4:
                    machineGun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 100;
                    fireRate = 0.2f;
                    break;
                case 5:
                    flameThrower.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 300;
                    bulletForce = 20;
                    fireRate = 0.005f;
                    break;
                case 6:
                    grenade.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 3;
                    break;
                default:
                    break;
            }
        }
    }  

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    equipClip.Play();

    //    uiAmmoCounter.SetActive(true);



    private void OnCollisionEnter2D(Collision2D other)
    {
        //GetComponent<AudioSource>().Play();
        //equipClip.Play();

        emptyMagazine = false;


        uiAmmoCounter.SetActive(true);
        

        emptyMagazine = false;

<<<<<<< HEAD
=======
        //Destroy(other.gameObject);

>>>>>>> Roopenbranch
        if (other.gameObject.tag == "PistolBox")
        {
            equippedGun = 1;
        }
        if (other.gameObject.tag == "ShotgunBox")
        {
            equippedGun = 2;
        }

        if (other.gameObject.tag == "Arbox")
        {
            equippedGun = 3;
        }

        if (other.gameObject.tag == "FTbox")
        {
            equippedGun = 5;
        }

        if (other.gameObject.tag == "Grenadebox")
        {
            equippedGun = 6;
        }

    }
}
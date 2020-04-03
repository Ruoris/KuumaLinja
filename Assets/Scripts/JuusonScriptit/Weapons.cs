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

    public AudioClip equipClip;

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
        FireControl();

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
                    break;
                case 1:
                    pistol.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 10;
                    break;
                case 2:
                    shotgun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 5;
                    break;
                case 3:
                    assaultRifle.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 30;
                    break;
                case 4:
                    machineGun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 100;
                    break;
                case 5:
                    flameThrower.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    ammoCapacity = 300;
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

    public void FireControl()
    {
        bulletForce = 40;

        if (equippedGun == 0)
        {
            fireRate = 0f;
        }
        if (equippedGun == 1)
        {
            fireRate = 0.3f;
        }
        if (equippedGun == 2)
        {
            fireRate = 1;
        }
        if (equippedGun == 3)
        {
            fireRate = 0.1f;
        }
        if (equippedGun == 4)
        {
            fireRate = 0.2f;
        }
        if (equippedGun == 5)
        {
            bulletForce = 20;
            fireRate = 0.005f;
        }
        if (equippedGun == 6)
        {
            bulletForce = 10;
            fireRate = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //GetComponent<AudioSource>().Play();
        //equipClip.Play();

        uiAmmoCounter.SetActive(true);
        

        emptyMagazine = false;

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
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{

    public GameObject pistol, shotgun, assaultRifle, machineGun, flameThrower, grenade;
    public GameObject player;
    public int bulletForce;
    public float fireRate;
    public bool emptyMagazine;
    public float reload = 0;

    public AudioClip equipClip;

    public GameObject pistolDrop, shotGunDrop, rifleDrop, gameThrow;

    public int equippedGun, ammoCapacity, ammoLeft, previousequippedGun, previousammoCapacity, previousAmmoLeft;
<<<<<<< HEAD
   
    // public GameObject speedBoost, ammoBoost, radiusBoost;
=======


>>>>>>> origin/TuomaksenBranch2

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
      
=======

>>>>>>> origin/TuomaksenBranch2

        equippedGun = Random.Range(1, 4);

        if (equippedGun == 1)
        {
            ammoCapacity = 10;
            ammoLeft = 10;
        }
        if (equippedGun == 2)
        {
            ammoCapacity = 5;
            ammoLeft = 5;
        }

        if (equippedGun == 3)
        {
            ammoCapacity = 30;
            ammoLeft = 30;
        }
        emptyMagazine = false;

    }

    // Update is called once per frame
    void Update()
    {
        EquipGun();
        FireControl();
<<<<<<< HEAD
=======

>>>>>>> origin/TuomaksenBranch2
        if (ammoLeft <= 0)
        {
            ammoLeft = ammoCapacity;
        }

    }

    void EquipGun()
    {

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
                    emptyMagazine = true;
                    ammoCapacity = 0;
                    break;
                case 1:
                    //pistol.SetActive(true);
                    break;
                case 2:


                    //shotgun.SetActive(true);
                    break;
                case 3:

                    //assaultRifle.SetActive(true);
                    break;
                case 4:
                    //machineGun.SetActive(true);
                    break;
            }
        }
    }

    public void FireControl()
    {
        bulletForce = 10;

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
    public void DropGun()
    {
<<<<<<< HEAD
     //int buffToActivate = Random.Range(0 , 10);
     //   if (buffToActivate < 2)
     //   {
     //       var droppedspeedBoost = Instantiate(speedBoost, gameThrow.transform.position, Quaternion.identity);
     //   }
     //   if (buffToActivate > 2&&buffToActivate<5)
     //   {
     //       var droppedradiusBoost = Instantiate(radiusBoost, gameThrow.transform.position, Quaternion.identity);
     //   }
     //   if (buffToActivate ==8)
     //   {
     //       var droppedammoBoost = Instantiate(ammoBoost, gameThrow.transform.position, Quaternion.identity);
     //   }
=======
>>>>>>> origin/TuomaksenBranch2
        if (equippedGun == 1)
        {
            Debug.Log("ase pudotettu");
            var droppedPistol = Instantiate(pistolDrop, gameThrow.transform.position, Quaternion.identity);
            droppedPistol.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedPistol.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;
<<<<<<< HEAD
           
=======

>>>>>>> origin/TuomaksenBranch2

        }
        if (equippedGun == 2)
        {
            var droppedShotgun = Instantiate(shotGunDrop, gameThrow.transform.position, Quaternion.identity);
            droppedShotgun.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedShotgun.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;
<<<<<<< HEAD
            
=======

>>>>>>> origin/TuomaksenBranch2

        }

        if (equippedGun == 3)
        {
            var droppedRifle = Instantiate(rifleDrop, gameThrow.transform.position, Quaternion.identity);
            droppedRifle.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedRifle.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;

<<<<<<< HEAD
           
=======

>>>>>>> origin/TuomaksenBranch2
        }
    }
}

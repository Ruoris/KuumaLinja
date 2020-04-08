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

    public GameObject uiAmmoCounter, uiShellCounter, uiRifleCounter, pistolDrop, shotGunDrop,rifleDrop,gameThrow;

    public int equippedGun, ammoCapacity, ammoLeft, previousequippedGun, previousammoCapacity, previousAmmoLeft;



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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGunWithButton();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropGun();
        }

   
        if (ammoLeft <= 0)
        {
            equippedGun = 0;
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
                    if (uiAmmoCounter.activeSelf == true)
                    {
                        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
                        uiAmmoCounter.SetActive(false);
                    }
                    if (uiShellCounter.activeSelf == true)
                    {
                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
                        uiShellCounter.SetActive(false);
                    }
                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
                        uiRifleCounter.SetActive(false);
                    }
                    playerBothHands.SetActive(false);
                    playerOneHand.SetActive(false);
                    playerMelee.SetActive(false);


                    emptyMagazine = true;
                    ammoCapacity = 0;
                    break;
                case 1:
                    if (uiShellCounter.activeSelf == true)
                    {

                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(5);
                        uiShellCounter.SetActive(false);
                    }
                    //if (uiAmmoCounter.activeSelf == true)
                    //{
                    //    uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(10);
                    //    uiAmmoCounter.SetActive(false);
                    //}
                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(30);
                        uiRifleCounter.SetActive(false);
                    }
                    uiAmmoCounter.SetActive(true);
                    pistol.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    //   ammoCapacity = 10;
                    break;
                case 2:
                    //if (uiShellCounter.activeSelf == true)
                    //{
                    //    uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(5);
                    //    uiShellCounter.SetActive(false);
                    //}
                    if (uiAmmoCounter.activeSelf == true)
                    {
                        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(10);
                        uiAmmoCounter.SetActive(false);
                    }
                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(30);
                        uiRifleCounter.SetActive(false);
                    }
                    uiShellCounter.SetActive(true);
                    shotgun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    // ammoCapacity = 5;
                    break;
                case 3:
                    if (uiShellCounter.activeSelf == true)
                    {
                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(5);
                        uiShellCounter.SetActive(false);
                    }
                    if (uiAmmoCounter.activeSelf == true)
                    {
                        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(10);
                        uiAmmoCounter.SetActive(false);
                    }
                    //if (uiRifleCounter.activeSelf == true)
                    //{
                    //    uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(30);
                    //    uiRifleCounter.SetActive(false);
                    //}
                    uiRifleCounter.SetActive(true);
                    assaultRifle.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    //   ammoCapacity = 30;
                    break;
                case 4:
                    machineGun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    //  ammoCapacity = 100;
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
    public void DropGun()
    {
        if (equippedGun == 1)
        {
            var droppedPistol = Instantiate(pistolDrop, gameThrow.transform.position, Quaternion.identity);
            droppedPistol.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedPistol.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;
            
            equippedGun = 0;
            ammoLeft = 0;
        }
        if (equippedGun == 2)
        {
            var droppedShotgun = Instantiate(shotGunDrop, gameThrow.transform.position, Quaternion.identity);
            droppedShotgun.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedShotgun.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;
          
            equippedGun = 0;
            ammoLeft = 0;
        }

        if (equippedGun == 3)
        {
            var droppedRifle = Instantiate(rifleDrop, gameThrow.transform.position, Quaternion.identity);
            droppedRifle.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedRifle.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;
          
            equippedGun = 0;
            ammoLeft = 0;
        }


    }

    public void SwitchGunWithButton()
    {

        Debug.Log("Q painettu");
        int tempammoCapacity = ammoCapacity;
        int tempAmmoLeft = ammoLeft;
        int tempEquippedweapon = equippedGun;
        if (equippedGun == 1)
        {
            uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
        }
        if (equippedGun == 2)
        {
            uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
        }
        if (equippedGun == 3)
        {
            uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(ammoCapacity);
        }
        ammoCapacity = previousammoCapacity;
        ammoLeft = previousAmmoLeft;
        equippedGun = previousequippedGun;
        if (equippedGun == 1)
        {
            uiAmmoCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
        }
        if (equippedGun == 2)
        {
            uiShellCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
        }
        if (equippedGun == 3)
        {
            uiRifleCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
        }

        previousammoCapacity = tempammoCapacity;
        previousAmmoLeft = tempAmmoLeft;
        previousequippedGun = tempEquippedweapon;


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //GetComponent<AudioSource>().Play();
        //equipClip.Play();




      

      

        if (other.gameObject.tag == "PistolBox")
        {
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;

            }
            emptyMagazine = false;
           
            equippedGun = 1;
            ammoCapacity = 10;
            ammoLeft = 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "PistolDrop")
        {
            Destroy(other.gameObject);
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;
            }
            emptyMagazine = false;
            uiAmmoCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
            equippedGun = 1;
            ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
            ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ShotgunBox")
        {
            emptyMagazine = false;
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;

            }

            
            equippedGun = 2;
            ammoCapacity = 5;
            ammoLeft = 5;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ShotGunDrop")
        {
            emptyMagazine = false;
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;
            }
            

            uiShellCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
            equippedGun = 2;
            ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
            ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Arbox")
        {
            emptyMagazine = false;
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;
            }
            
            equippedGun = 3;
            ammoCapacity = 30;
            ammoLeft = 30;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RifleDrop")
        {
            emptyMagazine = false;
            if (equippedGun != 0)
            {
                previousequippedGun = equippedGun;
                previousAmmoLeft = ammoLeft;
                previousammoCapacity = ammoCapacity;
            }

          
            uiRifleCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
            equippedGun = 3;
            ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
            ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "FTbox")
        {
            emptyMagazine = false;
            Destroy(other.gameObject);
            equippedGun = 5;
        }

        if (other.gameObject.tag == "Grenadebox")
        {
            Destroy(other.gameObject);
          
            equippedGun = 6;
        }
    }
}
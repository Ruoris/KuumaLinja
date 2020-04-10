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


    public GameObject uiAmmoCounter, uiShellCounter, uiRifleCounter, pistolDrop, shotGunDrop, rifleDrop, gameThrow;
    //public GameObject uiWeaponSelection;

    public int equippedGun, ammoCapacity, ammoLeft, previousEquippedGun, previousAmmoCapacity, previousAmmoLeft;
   



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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGunWithButton();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
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
        bulletForce = 30;

        uiAmmoCounter.SetActive(true);

        foreach (Transform weapon in player.GetComponentsInChildren<Transform>())
        {
            for (int i = 0; i < player.transform.childCount; i++)
            {
                // deactivates other weapons and stances
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
                    //uiWeaponSelection.SetActive(false);
                    emptyMagazine = true;
                    ammoCapacity = 0;

                    fireRate = 0f;

                    break;

                case 1:
                    if (uiShellCounter.activeSelf == true)
                    {
                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(5);
                        uiShellCounter.SetActive(false);
                    }

                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(30);
                        uiRifleCounter.SetActive(false);
                    }

                    uiAmmoCounter.SetActive(true);
                    pistol.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    fireRate = 0.3f;
                    ammoCapacity = 10;
                    break;
                case 2:
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
                    ammoCapacity = 5;
                    fireRate = 1;
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

                    uiRifleCounter.SetActive(true);
                    assaultRifle.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    fireRate = 0.1f;
                    ammoCapacity = 30;
                    break;
                    // alla piilotettuna lisää aseita
                //case 4:
                //    machineGun.SetActive(true);
                //    playerBothHands.SetActive(true);
                //    emptyMagazine = false;
                //    break;

                //case 5:
                //    flameThrower.SetActive(true);
                //    playerBothHands.SetActive(true);
                //    emptyMagazine = false;
                //    ammoCapacity = 300;
                //    fireRate = 0.005f;
                //    break;

                //case 6:
                //    grenade.SetActive(true);
                //    playerOneHand.SetActive(true);
                //    emptyMagazine = false;
                //    ammoCapacity = 3;
                //    bulletForce = 10;
                //    fireRate = 2f;
                //    break;

                //default:
                //    break;
            }
        }
    }  

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    equipClip.Play();

    //    uiAmmoCounter.SetActive(true);

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
        ammoCapacity = previousAmmoCapacity;
        ammoLeft = previousAmmoLeft;
        equippedGun = previousEquippedGun;
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

        previousAmmoCapacity = tempammoCapacity;
        previousAmmoLeft = tempAmmoLeft;
        previousEquippedGun = tempEquippedweapon;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //GetComponent<AudioSource>().Play();
        //equipClip.Play();


        if(other.gameObject.layer == 11) // 11 = Equipment
        {
            emptyMagazine = false;

            Destroy(other.gameObject);

            if (other.gameObject.tag == "PistolBox")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                equippedGun = 1;
                ammoCapacity = 10;
                ammoLeft = 10;
            }
            if (other.gameObject.tag == "PistolDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                uiAmmoCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                equippedGun = 1;
                ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            }
            if (other.gameObject.tag == "ShotgunBox")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                equippedGun = 2;
                ammoCapacity = 5;
                ammoLeft = 5;
            }
            if (other.gameObject.tag == "ShotGunDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                uiShellCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                equippedGun = 2;
                ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            }

            if (other.gameObject.tag == "Arbox")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                equippedGun = 3;
                ammoCapacity = 30;
                ammoLeft = 30;
            }
            if (other.gameObject.tag == "RifleDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }
                uiRifleCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                equippedGun = 3;
                ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
            }
            //if (other.gameObject.tag == "FTbox")
            //{
            //    equippedGun = 5;
            //}

            //if (other.gameObject.tag == "Grenadebox")
            //{
            //    GetComponent<Pshoot>().shotsFired = 0;
            //    equippedGun = 6;
            //}
        }
    }
}
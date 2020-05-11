﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapons : MonoBehaviour
{
    public GameObject pistol, shotgun, assaultRifle;
    public GameObject player, playerMelee, playerOneHand, playerBothHands, pipeHands;
    public int bulletForce;
    public float fireRate;
    public bool emptyMagazine;


    //public AudioClip equipClip;

    public GameObject uiAmmoCounter, uiShellCounter, uiRifleCounter, pistolDrop, shotGunDrop, rifleDrop, gameThrow, secondaryWeaponContainer;
    public GameObject[] otherWeapons;
    public Transform firePoint;


    public int equippedGun, ammoCapacity, ammoLeft, previousEquippedGun, previousAmmoCapacity, previousAmmoLeft;
    public int pistolCapacity = 10;
    public int shotgunCapacity = 5;
    public int assaultRifleCapacity = 15;



    // Start is called before the first frame update
    void Start()
    {   pistolCapacity = 10;
        shotgunCapacity = 5;
        assaultRifleCapacity = 15;


        emptyMagazine = true;
        ammoLeft = 0;
        secondaryWeaponContainer = GameObject.Find("Misc stuff/Canvas/otherWeapon/secondaryWeaponContainer");
        otherWeapons[0] = GameObject.Find("Misc stuff/Canvas/otherWeapon/uiPistol");
        otherWeapons[1] = GameObject.Find("Misc stuff/Canvas/otherWeapon/uiShotgun");
        otherWeapons[2] = GameObject.Find("Misc stuff/Canvas/otherWeapon/uiAR");
        uiAmmoCounter = GameObject.Find("Misc stuff/Canvas/Panel/uiBulletCounter");
        uiShellCounter = GameObject.Find("Misc stuff/Canvas/Panel/uiShotgunShellCounter");
        uiRifleCounter = GameObject.Find("Misc stuff/Canvas/Panel/uiRifleCounter");
        AmmoBuff();
        RadiusIncrease();
        MovementSpeedBuff();
    }

    // Update is called once per frame
    void Update()
    {
        SecondaryWeapon();
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

    void SecondaryWeapon()
    {

        if (previousEquippedGun == 0)
        {
            secondaryWeaponContainer.SetActive(false);
            otherWeapons[0].SetActive(false);
            otherWeapons[1].SetActive(false);
            otherWeapons[2].SetActive(false);
        }
        if (previousEquippedGun == 1)
        {
            secondaryWeaponContainer.SetActive(true);
            otherWeapons[0].SetActive(true);
            otherWeapons[1].SetActive(false);
            otherWeapons[2].SetActive(false);
        }
        if (previousEquippedGun == 2)
        {
            secondaryWeaponContainer.SetActive(true);
            otherWeapons[0].SetActive(false);
            otherWeapons[1].SetActive(true);
            otherWeapons[2].SetActive(false);
        }
        if (previousEquippedGun == 3)
        {
            secondaryWeaponContainer.SetActive(true);
            otherWeapons[0].SetActive(false);
            otherWeapons[1].SetActive(false);
            otherWeapons[2].SetActive(true);
        }





    }

    void EquipGun()
    {
        pipeHands.SetActive(false);

        bulletForce = 30;

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
                    pipeHands.SetActive(true);

                    bool melee = GetComponent<Pshoot>().melee;
                    if (melee == false)
                    {
                        pipeHands.SetActive(true);
                    }

                    //playerMelee.SetActive(false);
                    //uiWeaponSelection.SetActive(false);
                    emptyMagazine = true;
                    ammoCapacity = 0;
                    break;

                case 1:
                    if (uiShellCounter.activeSelf == true)
                    {
                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(shotgunCapacity);
                        uiShellCounter.SetActive(false);
                    }

                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(assaultRifleCapacity);
                        uiRifleCounter.SetActive(false);
                    }

                    uiAmmoCounter.SetActive(true);
                    pistol.SetActive(true);
                    playerOneHand.SetActive(true);
                    emptyMagazine = false;
                    fireRate = 0.3f;

                    break;
                case 2:
                    if (uiAmmoCounter.activeSelf == true)
                    {
                        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(pistolCapacity);
                        uiAmmoCounter.SetActive(false);
                    }
                    if (uiRifleCounter.activeSelf == true)
                    {
                        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(assaultRifleCapacity);
                        uiRifleCounter.SetActive(false);
                    }
                    uiShellCounter.SetActive(true);
                    shotgun.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    fireRate = 1;
                    break;

                case 3:
                    if (uiShellCounter.activeSelf == true)
                    {
                        uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(shotgunCapacity);
                        uiShellCounter.SetActive(false);
                    }
                    if (uiAmmoCounter.activeSelf == true)
                    {
                        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(pistolCapacity);
                        uiAmmoCounter.SetActive(false);
                    }

                    uiRifleCounter.SetActive(true);
                    assaultRifle.SetActive(true);
                    playerBothHands.SetActive(true);
                    emptyMagazine = false;
                    fireRate = 0.1f;
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

    public void DropGun()
    {

        GameObject ammoSpawn = GameObject.FindWithTag("AmmoSpawn");
        Transform firePoint = ammoSpawn.GetComponent<Transform>();

        if (equippedGun == 1)
        {
            var droppedPistol = Instantiate(pistolDrop, gameThrow.transform.position, Quaternion.identity);
            droppedPistol.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedPistol.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;


            Rigidbody2D rb1 = droppedPistol.GetComponent<Rigidbody2D>();
            float spreadAngle = Random.Range(19, 5);
            var x = firePoint.position.x - player.transform.position.x;
            var y = firePoint.position.y - player.transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
            rb1.AddForce(MovementDirection * 2, ForceMode2D.Impulse);

            equippedGun = 0;
            ammoLeft = 0;
        }
        if (equippedGun == 2)
        {
            var droppedShotgun = Instantiate(shotGunDrop, gameThrow.transform.position, Quaternion.identity);
            droppedShotgun.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedShotgun.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;


            Rigidbody2D rb1 = droppedShotgun.GetComponent<Rigidbody2D>();
            float spreadAngle = Random.Range(19, 5);
            var x = firePoint.position.x - player.transform.position.x;
            var y = firePoint.position.y - player.transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
            rb1.AddForce(MovementDirection * 2, ForceMode2D.Impulse);
            equippedGun = 0;
            ammoLeft = 0;
        }

        if (equippedGun == 3)
        {
            var droppedRifle = Instantiate(rifleDrop, gameThrow.transform.position, Quaternion.identity);
            droppedRifle.GetComponent<WeaponDrop>().bulletsLeft = ammoLeft;
            droppedRifle.GetComponent<WeaponDrop>().ammoCapacity = ammoCapacity;


            Rigidbody2D rb1 = droppedRifle.GetComponent<Rigidbody2D>();
            float spreadAngle = Random.Range(19, 5);
            var x = firePoint.position.x - player.transform.position.x;
            var y = firePoint.position.y - player.transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
            rb1.AddForce(MovementDirection * 2, ForceMode2D.Impulse);
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
            uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(pistolCapacity);
        }
        if (equippedGun == 2)
        {
            uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(shotgunCapacity);
        }
        if (equippedGun == 3)
        {
            uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(assaultRifleCapacity);
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
    public void MovementSpeedBuff()
    {
        player.GetComponent<PlayerCtrl>().movementSpeed =1.8f+GameStatus.status.movementSpeedAmount ;
    }
    public void AmmoBuff()
    {   uiShellCounter.GetComponent<AmmocounterScript>().ReturnColor(shotgunCapacity);
        uiAmmoCounter.GetComponent<AmmocounterScript>().ReturnColor(pistolCapacity);
       
        uiRifleCounter.GetComponent<AmmocounterScript>().ReturnColor(assaultRifleCapacity);
        pistolCapacity =10+GameStatus.status.ammobuffAmount;
        shotgunCapacity = 5+ GameStatus.status.ammobuffAmount;
        assaultRifleCapacity = 15+ GameStatus.status.ammobuffAmount;


        if (equippedGun == 1)
        {
            ammoCapacity = 10+GameStatus.status.ammobuffAmount;
            ammoLeft = ammoCapacity;

        }
        if (equippedGun == 2)
        {
            ammoCapacity = 5+ GameStatus.status.ammobuffAmount;
            ammoLeft = ammoCapacity;

        }

        if (equippedGun == 3)
        {
            ammoCapacity = 15+ GameStatus.status.ammobuffAmount;
            ammoLeft = ammoCapacity;

        }
        if (previousEquippedGun == 1)
        {
            previousAmmoCapacity = 10+ GameStatus.status.ammobuffAmount;
            previousAmmoLeft = previousAmmoCapacity;
        }
        if (previousEquippedGun == 2)
        {
            previousAmmoCapacity = 5+ GameStatus.status.ammobuffAmount;
            previousAmmoLeft = previousAmmoCapacity;
        }

        if (previousEquippedGun == 3)
        {
            previousAmmoCapacity = 15+ GameStatus.status.ammobuffAmount;
            previousAmmoLeft = previousAmmoCapacity;

        }

      
        for (int i = 5; i < shotgunCapacity; i++)
        {

            var child = uiShellCounter.transform.GetChild(i).gameObject;


            if (child != null && child.name.Contains("shell5"))
            {
                child.SetActive(true);
            }
        } 
        for (int i = 10; i < pistolCapacity; i++)
        {

            var child = uiAmmoCounter.transform.GetChild(i).gameObject;


            if (child != null && child.name.Contains("bullet"))
            {
                child.SetActive(true);
            }
        }
        for (int i = 15; i <assaultRifleCapacity; i++)
        {

            var child = uiRifleCounter.transform.GetChild(i).gameObject;


            if (child != null && child.name.Contains("rifle"))
            {
                child.SetActive(true);
            }
        }
    }
    public void RadiusIncrease()
    {
        GetComponent<PikkuFOV>().viewRadius = 0.5f+GameStatus.status.radiusIncreaseAmount;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 11 && Input.GetKey(KeyCode.E)) // 11 = Equipment
        {


            emptyMagazine = false;



            if (other.gameObject.tag == "PistolDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }


                equippedGun = 1;
                ammoCapacity = pistolCapacity;
                ammoLeft = pistolCapacity;
                uiAmmoCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "ShotGunDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }


                equippedGun = 2;
                ammoCapacity = shotgunCapacity;
                ammoLeft = shotgunCapacity;
                uiShellCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "RifleDrop")
            {
                if (equippedGun != 0)
                {
                    previousEquippedGun = equippedGun;
                    previousAmmoLeft = ammoLeft;
                    previousAmmoCapacity = ammoCapacity;
                }

                equippedGun = 3;
                ammoCapacity =assaultRifleCapacity;
                ammoLeft = assaultRifleCapacity;
                uiRifleCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(ammoCapacity, ammoLeft);
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Grenadebox")
            {
                GetComponent<Pshoot>().shotsFired = 0;
                equippedGun = 6;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 11) // 11 = Equipment
        {
            //Debug.Log("collide");
            //emptyMagazine = false;
            if (other.gameObject.tag == "Scientist" && Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Scientist>().Doping();

            }
            if (other.gameObject.tag == "Speedboost")
            {
                pistolCapacity = 15;
                for (int i = 10; i < uiAmmoCounter.transform.childCount; i++)
                {
                    // deactivates other weapons and stances
                    var child = uiAmmoCounter.transform.GetChild(i).gameObject;

                    if (child != null && child.name.Contains("bullet"))
                    {
                        child.SetActive(true);
                    }
                }

            }
            //GetComponent<AudioSource>().Play();
            //equipClip.Play();
            if (other.collider.tag == "LevelEnd")
            {
                Cursor.visible = true;
                Debug.Log("levelComplete");
                Scene scene = SceneManager.GetActiveScene();
                if (scene.buildIndex == 2)
                {
                    GameStatus.status.Level1 = true;
                }
                if (scene.buildIndex == 3)
                {
                    GameStatus.status.Level2 = true;
                }

                SceneManager.LoadScene("UIMainmenukehitysScene");
                AudioImmortality.immortal.ChangeBackgroundMusic("UIMainmenukehitysScene");
                GameStatus.status.currentLevel = "UIMainmenukehitysScene";
                GameStatus.status.Save();
            }
            if (other.gameObject.layer == 11) // 11 = Equipment
            {
                //Debug.Log("collide");
                //emptyMagazine = false;



                //if (other.gameObject.tag == "PistolBox")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }

                //    equippedGun = 1;
                //    ammoCapacity = 10;
                //    ammoLeft = 10;
                //    Destroy(other.gameObject);
                //}
                //if (other.gameObject.tag == "PistolDrop")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }

                //    uiAmmoCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                //    equippedGun = 1;
                //    ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                //    ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
                //    Destroy(other.gameObject);
                //}
                //if (other.gameObject.tag == "ShotgunBox")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }

                //    equippedGun = 2;
                //    ammoCapacity = 5;
                //    ammoLeft = 5;
                //    Destroy(other.gameObject);
                //}
                //if (other.gameObject.tag == "ShotGunDrop")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }

                //    uiShellCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                //    equippedGun = 2;
                //    ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                //    ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
                //    Destroy(other.gameObject);
                //}

                //if (other.gameObject.tag == "Arbox")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }

                //    equippedGun = 3;
                //    ammoCapacity = 30;
                //    ammoLeft = 30;
                //    Destroy(other.gameObject);
                //}
                //if (other.gameObject.tag == "RifleDrop")
                //{
                //    if (equippedGun != 0)
                //    {
                //        previousEquippedGun = equippedGun;
                //        previousAmmoLeft = ammoLeft;
                //        previousAmmoCapacity = ammoCapacity;
                //    }
                //    uiRifleCounter.GetComponent<AmmocounterScript>().PartialColorToUsed(other.gameObject.GetComponent<WeaponDrop>().ammoCapacity, other.gameObject.GetComponent<WeaponDrop>().bulletsLeft);
                //    equippedGun = 3;
                //    ammoCapacity = other.gameObject.GetComponent<WeaponDrop>().ammoCapacity;
                //    ammoLeft = other.gameObject.GetComponent<WeaponDrop>().bulletsLeft;
                //    Destroy(other.gameObject);
                //}
                if (other.gameObject.tag == "FTbox")
                {

                    equippedGun = 5;
                }

                if (other.gameObject.tag == "Grenadebox")
                {
                    GetComponent<Pshoot>().shotsFired = 0;
                    equippedGun = 6;
                }
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshoot : MonoBehaviour
{
    public float bulletForce;
    public int shotsFired;
    private float fireRate;
    private float canFire;

    public AudioSource gunSound;

    public Transform firePoint;
    public GameObject player;
    public GameObject bulletprefab, flameprefab, grenadeprefab, gunFlareAnimation, explosion;
    public GameObject meleeAnimation, pipeHands;
    private GameObject ammopanel;
    public bool melee;

    
    public GameObject dialogueTrigger;
 

    void Update()
    {
        meleeAnimation.transform.position = player.transform.position;
        meleeAnimation.transform.rotation = player.transform.rotation;

        GameObject pauser = GameObject.FindWithTag("soundsettings");
        int equippedGun = GetComponent<Weapons>().equippedGun;
        bool emptyMagazine = GetComponent<Weapons>().emptyMagazine;
        gunFlareAnimation.SetActive(false);

        if (emptyMagazine)
        {
            // shotsFired = 0;
        }

        
        if (Input.GetButton("Fire1") && fireRate < canFire && pauser.GetComponent<Pause>().paused == false)
        {
            if (equippedGun != 0)

            {
                ammopanel = GameObject.FindWithTag("activeBulletCounter");
                gunSound.Play();
                Fire();
                gunFlareAnimation.SetActive(true);

                GetComponent<Weapons>().ammoLeft--;
                AmmoCounter(equippedGun);
                if (equippedGun == 2)
                {
                    // if a shotgun is equipped
                    Fire();
                    Fire();
                    Fire();
                    Fire();
                    Fire();
                    Fire();
                }
            }
            else
            {
                melee = true;
                meleeAnimation.SetActive(true);
                pipeHands.SetActive(false);
                StartCoroutine("MeleeAnimation", 0.25f);
            }
        }
        canFire += Time.deltaTime;
    }

    IEnumerator MeleeAnimation(float delay)
    {
        while(true && melee == true)
        {
            //Debug.Log("melee");

            yield return new WaitForSeconds(delay);
            meleeAnimation.SetActive(false);
            pipeHands.SetActive(true);
            melee = false;
        }

    }

    void AmmoCounter(int equippedGun)
    {
        ammopanel = GameObject.FindWithTag("activeBulletCounter");

        ammopanel.GetComponent<AmmocounterScript>().ChangeColor(GetComponent<Weapons>().ammoLeft);

    }
    void Fire()

    {
        canFire = 0;

        int equippedGun = GetComponent<Weapons>().equippedGun;
        fireRate = GetComponent<Weapons>().fireRate;
        bulletForce = GetComponent<Weapons>().bulletForce;

        var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);

        Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

        // sets the random spread of the weapons
        float spreadAngle = Random.Range(8, 18);

        if (equippedGun == 2)
        {
            spreadAngle = Random.Range(2, 22);
        }

        var x = firePoint.position.x - player.transform.position.x;
        var y = firePoint.position.y - player.transform.position.y;
        float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

        var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

        tempBulletRB.velocity = MovementDirection * bulletForce;

        if (equippedGun != 6)
        {
            Destroy(tempBullet, 0.3f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshoot : MonoBehaviour
{
    public float bulletForce;
    public int shotsFired;
    private float fireRate;
    private float canFire;
    public float counter;
    public float explosionCounter = 3;
    public AudioSource gunSound;

    public Transform firePoint;
    public GameObject player;
    public GameObject bulletprefab, flameprefab, grenadeprefab, gunFlareAnimation, explosion;
    private GameObject ammopanel;

    

    void Start()
    {
        //shotsFired = 0;
        explosionCounter = 3;
    }

    void Update()
    {
        int equippedGun = GetComponent<Weapons>().equippedGun;
        bool emptyMagazine = GetComponent<Weapons>().emptyMagazine;
        gunFlareAnimation.SetActive(false);

        if (emptyMagazine)
        {
            // shotsFired = 0;
        }

        if (Input.GetButton("Fire1") && fireRate < canFire && !emptyMagazine)
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
        canFire += Time.deltaTime;
    }

void AmmoCounter(int equippedGun)
{
    ammopanel = GameObject.FindWithTag("activeBulletCounter");

    ammopanel.GetComponent<AmmocounterScript>().ChangeColor();

}

void Fire()
    {
        canFire = 0;
        counter = 0;

        int equippedGun = GetComponent<Weapons>().equippedGun;
        fireRate = GetComponent<Weapons>().fireRate;
        bulletForce = GetComponent<Weapons>().bulletForce;

        var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);

        if (equippedGun == 5 || equippedGun == 6)
        {
            gunSound.Stop();
            // if weapon is flamethrower or grenade

            tempBullet = (GameObject)Instantiate(flameprefab, firePoint.position, firePoint.rotation);
            //if (equippedGun == 6)
            //{
            //    tempBullet = (GameObject)Instantiate(grenadeprefab, firePoint.position, firePoint.rotation);

            //    counter += Time.deltaTime;

            //    if(counter >= explosionCounter)
            //    {
            //        Instantiate(explosion, grenadeprefab.transform.position, Quaternion.identity);
            //    }
            //    Destroy(tempBullet, 3.1f);
            //}

            GameObject duplicate = GameObject.Find("bullet(Clone)");
            if (duplicate)
            {
                Destroy(duplicate.gameObject);
            }
        }

        Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

        // sets the random spread of the weapons
        float spreadAngle = Random.Range(19, 5);

        var x = firePoint.position.x - player.transform.position.x;
        var y = firePoint.position.y - player.transform.position.y;
        float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

        var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

        tempBulletRB.velocity = MovementDirection * bulletForce;

        if(equippedGun != 6)
        {
            Destroy(tempBullet, 0.3f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPshoot : MonoBehaviour
{
    public float bulletForce;
    
    private float fireRate;
    private float canFire;
    public float counter;
    public float explosionCounter = 3;
    public AudioSource gunSound;

    public Transform firePoint;
    public GameObject player;
    public GameObject bulletprefab, flameprefab, grenadeprefab, gunFlareAnimation, explosion;

    public float readyToFire = 0;  
    private bool waitOver = false;

    void Start()
    {
        readyToFire = Random.Range(0.5f, 2);
        explosionCounter = 3;
    }

    void Update()
    {
        GameObject pauser = GameObject.FindWithTag("soundsettings");
        int equippedGun = GetComponent<EnemyWeapons>().equippedGun;
        bool emptyMagazine = GetComponent<EnemyWeapons>().emptyMagazine;
        gunFlareAnimation.SetActive(false);
        bool pursuing = GetComponent<EnemyController>().GetPursuing();
        
        if (pursuing== true)
        {
            readyToFire -= Time.deltaTime;
            if (readyToFire<0) 
            { 
                waitOver = true; 
            }
        }
        //else
        //{
        //    readyToFire = 0;
        //}
        if (waitOver==true && pursuing == true && fireRate < canFire && !emptyMagazine && pauser.GetComponent<Pause>().paused == false)
        {

            Fire();
            gunFlareAnimation.SetActive(true);

            GetComponent<EnemyWeapons>().ammoLeft--;

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

    void Fire()
    {
        canFire = 0;
        counter = 0;
        gunSound.Play();

        int equippedGun = GetComponent<EnemyWeapons>().equippedGun;
        fireRate = GetComponent<EnemyWeapons>().fireRate;
        bulletForce = GetComponent<EnemyWeapons>().bulletForce;

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

        if (equippedGun != 6)
        {
            Destroy(tempBullet, 0.3f);
        }
    }
}

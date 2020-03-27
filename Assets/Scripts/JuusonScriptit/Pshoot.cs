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

    public Transform firePoint;
    public GameObject player;
    public GameObject bulletprefab, flameprefab, grenadeprefab, explosion;

    

    void Start()
    {
        shotsFired = 0;
        explosionCounter = 3;
    }

    void Update()
    {
        int equippedGun = GetComponent<Weapons>().equippedGun;
        bool emptyMagazine = GetComponent<Weapons>().emptyMagazine;

        if(emptyMagazine)
        {
            shotsFired = 0;
        }

        if (Input.GetButton("Fire1") && fireRate < canFire && equippedGun != 0 && !emptyMagazine)
        {
            Fire();

            shotsFired++;

            if (equippedGun == 2)
            {
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

        int equippedGun = GetComponent<Weapons>().equippedGun;
        fireRate = GetComponent<Weapons>().fireRate;
        bulletForce = GetComponent<Weapons>().bulletForce;

        var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);

        if (equippedGun == 5 || equippedGun == 6)
        {
            tempBullet = (GameObject)Instantiate(flameprefab, firePoint.position, firePoint.rotation);
            if (equippedGun == 6)
            {
                tempBullet = (GameObject)Instantiate(grenadeprefab, firePoint.position, firePoint.rotation);

                counter += Time.deltaTime;

                if(counter >= explosionCounter)
                {
                    Instantiate(explosion, grenadeprefab.transform.position, Quaternion.identity);
                }
                Destroy(tempBullet, 3.1f);
            }

            GameObject duplicate = GameObject.Find("bullet(Clone)");
            if (duplicate)
            {
                Destroy(duplicate.gameObject);
            }
        }

        Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

        float spreadAngle = Random.Range(10, -10);

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
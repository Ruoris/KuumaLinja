using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshoot : MonoBehaviour
{
    public float bulletForce;
    public int shotsFired;
    private float fireRate;
    private float canFire;

    public Transform firePoint;
    public GameObject bulletprefab, flameprefab, player;

    

    void Start()
    {
        shotsFired = 0;
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
        int equippedGun = GetComponent<Weapons>().equippedGun;
        fireRate = GetComponent<Weapons>().fireRate;
        bulletForce = GetComponent<Weapons>().bulletForce;

        var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);

        if (equippedGun == 5)
        {
            tempBullet = (GameObject)Instantiate(flameprefab, firePoint.position, firePoint.rotation);
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
        Destroy(tempBullet, 0.3f);
    }
}
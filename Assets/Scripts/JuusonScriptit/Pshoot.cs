using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshoot : MonoBehaviour
{
    public float bulletForce;
    private float fireRate = 0.3f;
    private float canFire;

    public Transform firePoint, firePoint2, firePoint3;
    public GameObject bulletprefab, player;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        int equippedGun = GetComponent<PlayerCtrl>().equippedGun;

        if (equippedGun == 1)
        {
            fireRate = 0.3f;
        }
        if (equippedGun == 3 || equippedGun == 5)
        {
            fireRate = 0.1f;
        }
        if (equippedGun == 4)
        {
            fireRate = 0.2f;
        }

        if (Input.GetButton("Fire1") && fireRate < canFire)
        {
            Fire();
            if(equippedGun == 2)
            {
                fireRate = 0.8f;
                Fire();
                Fire();
                Fire();
                Fire();
                Fire();
                Fire();
            }
        }
        canFire = canFire + Time.deltaTime;
    }

    void Fire()
    {
        canFire = 0;

        var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);

        Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

        float spreadAngle = Random.Range(10, -10);

        var x = firePoint.position.x - player.transform.position.x;
        var y = firePoint.position.y - player.transform.position.y;
        float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

        var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

        tempBulletRB.velocity = MovementDirection * bulletForce;
        Destroy(tempBullet, 3.0f);
    }
}

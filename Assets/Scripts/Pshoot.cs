using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshoot : MonoBehaviour
{
    public float bulletForce;
    private float fireRate = 0.5f;
    private float canFire;

    public Transform firepoint;
    public GameObject bulletPF;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && fireRate < canFire)
        {
            Fire();
        }
        canFire = canFire + Time.deltaTime;
    }

    void Fire()
    {
        canFire = 0;

        GameObject bullet = Instantiate(bulletPF, firepoint.position, firepoint.rotation);
        Rigidbody2D bulRB = bullet.GetComponent<Rigidbody2D>();
        bulRB.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
}

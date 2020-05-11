using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPshoot : MonoBehaviour
{

    public float bulletForce;

    private float fireRate;
    private float canFire;
    public bool readyToShoot;
    public float counter;
    public AudioSource gunSound;

    public Transform firePoint;
    public GameObject enemy;
    public GameObject bulletprefab, gunFlareAnimation;



    void Start()
    {
        readyToShoot = false;
    }

    void Update()
    {
        int equippedGun = GetComponent<EnemyWeapons>().equippedGun;
        bool emptyMagazine = GetComponent<EnemyWeapons>().emptyMagazine;
        gunFlareAnimation.SetActive(false);
        bool pursuing = GetComponent<EnemyController>().GetPursuing();

        if (pursuing == true && fireRate < canFire && !emptyMagazine)
        {

            Fire();
            gunFlareAnimation.SetActive(true);



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
        GetComponent<EnemyWeapons>().ammoLeft--;
        int equippedGun = GetComponent<EnemyWeapons>().equippedGun;
        fireRate = GetComponent<EnemyWeapons>().fireRate;
        bulletForce = GetComponent<EnemyWeapons>().bulletForce;
        bool playerDetected = GetComponent<EnemyController>().playerDetected;
        if (playerDetected && !readyToShoot)
        {
            StartCoroutine("FireCooldown");
        }

        if (readyToShoot)
        {


            var tempBullet = (GameObject)Instantiate(bulletprefab, firePoint.position, Quaternion.identity);

            Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

            // sets the random spread of the weapons
            float spreadAngle = Random.Range(-10, 10);

            var x = firePoint.position.x - enemy.transform.position.x;
            var y = firePoint.position.y - enemy.transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

            tempBulletRB.velocity = MovementDirection * bulletForce;

            Destroy(tempBullet, 0.3f);
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        readyToShoot = true;
    }
}

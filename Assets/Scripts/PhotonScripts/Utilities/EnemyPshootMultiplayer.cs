﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class EnemyPshootMultiplayer : MonoBehaviour
{

    public float bulletForce;

    private float fireRate;
    private float canFire;
    public bool readyToShoot;
    public float counter;
    public AudioSource gunSound;

    public Transform firePoint;
    public GameObject enemy;
    public GameObject bulletprefab;
    public PhotonView photonview;
    public Scene scene;

    void Start()
    {
        readyToShoot = false;
        scene = SceneManager.GetActiveScene();
        photonview = PhotonView.Get(this);
    }

    void Update()
    {
        int equippedGun = GetComponent<EnemyWeaponsMultiplayer>().equippedGun;
        bool emptyMagazine = GetComponent<EnemyWeaponsMultiplayer>().emptyMagazine;
       
            bool pursuing = GetComponent<EnemyControllerMultiplayer>().GetPursuing();

            if (pursuing == true && fireRate < canFire && !emptyMagazine)
            {
                photonview.RPC("FireMultiplayer", RpcTarget.All);
                //FireMultiplayer();




                if (equippedGun == 2)
                {
                    // if a shotgun is equipped
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                    photonview.RPC("FireMultiplayer", RpcTarget.All);   //FireMultiplayer();
                }
            }

        canFire += Time.deltaTime;
    }

    [PunRPC]
    void FireMultiplayer()
    {

        int equippedGun = GetComponent<EnemyWeaponsMultiplayer>().equippedGun;
        fireRate = GetComponent<EnemyWeaponsMultiplayer>().fireRate;
        bulletForce = GetComponent<EnemyWeaponsMultiplayer>().bulletForce;

        bool playerDetected = GetComponent<EnemyControllerMultiplayer>().playerDetected;
        if (playerDetected && !readyToShoot)
        {
            StartCoroutine("FireCooldown");
        }

        if (readyToShoot)
        {
            GetComponent<EnemyWeaponsMultiplayer>().ammoLeft--;
            canFire = 0;
            counter = 0;
            gunSound.Play();

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
        yield return new WaitForSeconds(3.5f);
        readyToShoot = true;
    }
}

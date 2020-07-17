using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPshoot : MonoBehaviourPun
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

    public PhotonView photonview;
    public GameObject dialogueTrigger;

    void Start()
    {
        photonview = PhotonView.Get(this);
    }
        void Update()
    {       meleeAnimation.transform.position = player.transform.position;
            meleeAnimation.transform.rotation = player.transform.rotation;
        if (base.photonView.IsMine)
        {

           

            GameObject pauser = GameObject.FindWithTag("soundsettings");
            int equippedGun = GetComponent<MultiplayerWeapons>().equippedGun;
            bool emptyMagazine = GetComponent<MultiplayerWeapons>().emptyMagazine;
            gunFlareAnimation.SetActive(false);

            if (emptyMagazine)
            {
                // shotsFired = 0;
            }


            if (Input.GetButton("Fire1") && fireRate < canFire)
            {
                if (equippedGun != 0)

                {
                    ammopanel = GameObject.FindWithTag("activeBulletCounter");
                   
                    gunSound.Play();
                    photonview.RPC("Fire", RpcTarget.All);
                   
                    gunFlareAnimation.SetActive(true);

                    GetComponent<MultiplayerWeapons>().ammoLeft--;
                    AmmoCounter(equippedGun);
                    if (equippedGun == 2)
                    {
                        // if a shotgun is equipped
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                        photonview.RPC("Fire", RpcTarget.All);   //Fire();
                    }
                }
                else
                {
                    photonview.RPC("Smash", RpcTarget.All);
                }
            }
           

        }
        canFire += Time.deltaTime;
    }

    IEnumerator MeleeAnimation(float delay)
    {
        
            while (true && melee == true)
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
        if (base.photonView.IsMine)
        {
            ammopanel = GameObject.FindWithTag("activeBulletCounter");

            ammopanel.GetComponent<AmmocounterScript>().ChangeColor(GetComponent<MultiplayerWeapons>().ammoLeft);
        }
    }
   [PunRPC]
   void Smash()
    {
                    melee = true;
                    meleeAnimation.SetActive(true);
                    pipeHands.SetActive(false);
                    StartCoroutine("MeleeAnimation", 0.25f);
    }
    [PunRPC]
    void Fire()
    {
        
            canFire = 0;

            int equippedGun = GetComponent<MultiplayerWeapons>().equippedGun;
            fireRate = GetComponent<MultiplayerWeapons>().fireRate;
            bulletForce = GetComponent<MultiplayerWeapons>().bulletForce;

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
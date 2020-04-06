﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D enemyRb;

    public Vector3 playerLastPosition;
    RaycastHit2D rayToPlayer;
    public float speed = 0.75f;
    public float detectionDistance = 10f;

    bool moving = true, patrolling = true, pursuing = false, hasGun = false, goingtoweapon = false, goingtolastloc = false;
    public bool clockwise = false, stationary = false;
    public int enemyType = 1; 

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPosition = player.transform.position;
        enemyRb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
        PlayerDetect();
    }

    void KilledByBullet()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.gameObject.CompareTag("Bullet"))
        {
            KilledByBullet();
        }
    }

    void Movement()
    {
        float distance = Vector2.Distance(player.transform.position, enemyRb.transform.position);
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        rayToPlayer = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(direction.x, direction.y), distance);
        Debug.DrawRay(transform.position, direction, Color.red);
        Vector3 f = transform.TransformDirection(Vector3.right);
        RaycastHit2D obstacleCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(f.x, f.y), 1.2f);
        

        if (moving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (pursuing)
        {
            speed = 1.0f;
            enemyRb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPosition.y - transform.position.y), 
                (playerLastPosition.x - transform.position.x)) * Mathf.Rad2Deg);

            if (rayToPlayer.collider.gameObject.CompareTag("Player"))
            {
                playerLastPosition = player.transform.position;
            }
        }

        if (goingtolastloc)
        {
            speed = 0.75f;
            enemyRb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPosition.y - transform.position.y),
                (playerLastPosition.x - transform.position.x)) * Mathf.Rad2Deg);

            if (Vector3.Distance(transform.position, playerLastPosition) < 1.5f)
            {

                if(clockwise)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                } else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                patrolling = true;
                goingtolastloc = false;
            }
        }

        if (patrolling)
        {
            speed = 2.0f;

            if (obstacleCheck.collider != null)
            {

                if (obstacleCheck.collider.gameObject.CompareTag("Wall"))
                {
                    if (clockwise)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }

            }
        }

    }

    void PlayerDetect()
    {

        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        Debug.Log("POS: "+ pos + "distance: " + Vector3.Distance(transform.position, player.transform.position));
        if (rayToPlayer.collider != null)
          
        {

            Debug.Log("hit collider hit: "+ rayToPlayer.collider.gameObject.ToString());
            if (rayToPlayer.collider.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position)<detectionDistance)
            {
                patrolling = false;
                pursuing = true;
                goingtoweapon = false;
                Debug.Log("player detected");
            } 
            else
            {
                if (pursuing)
                {
                    goingtolastloc = true;
                    pursuing = false;
                    goingtoweapon = false;
                }
            }
        }
    }
}

            
            
                

    
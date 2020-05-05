using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player, enemy, aseDroppisjiainti, pistolDrop;
    public GameObject enemySprite, footPrints, enemyWeapon;
    public GameObject death, death2;
    Rigidbody2D enemyRb;
    public int ammoCapacity, ammoLeft;
    public Vector3 playerLastPosition;
    RaycastHit2D rayToPlayer;
    public float speed;
    float wallDetectionDistance = 0.2f;
    float playerDetectionDistance = 2f;
    float distanceFromLastLocation = 1f; //kuinka pitkälle vihollinen jahtaa pelaajaa
    public GameObject collisionDetector;

    bool moving = true, patrolling = true, pursuing = false, goingtoweapon = false, goingtolastloc = false;
    public bool hasGun = false;
    public bool clockwise = false, stationary = false;
    public bool dying;
    public bool backToPatrol;
    public bool playerDetected;

    public int randomDirection = 4;


    public GameObject walkAnimation;

    void Start()
    {
        enemyWeapon.SetActive(false);
        Physics2D.queriesStartInColliders = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPosition = player.transform.position;
        enemyRb = this.GetComponent<Rigidbody2D>();
    }

    public bool GetPursuing()
    {
        return this.pursuing;
    }
    void FixedUpdate()
    {
        Movement();
        PlayerDetect();
        if (gameObject.GetComponent<SpriteRenderer>().enabled == true && Time.time > 1)
        {
            enemyWeapon.SetActive(true);
            footPrints.SetActive(true);
            enemySprite.SetActive(true);
            footPrints.transform.position = enemy.transform.position;
            footPrints.transform.rotation = enemy.transform.rotation;
        }
        else
        {
            enemyWeapon.SetActive(false);
            enemySprite.SetActive(false);
            footPrints.transform.parent = null;
        }
    }

    void KilledByBullet()
    {
        int deathInt = Random.Range(1, 3);

        Destroy(footPrints);
        GetComponent<EnemyWeapons>().DropGun();

        enemy.transform.eulerAngles = new Vector3(0, 0, enemy.transform.eulerAngles.z - 180);
        
        if (deathInt == 2)
        {
            Instantiate(death2, enemy.transform.position, enemy.transform.rotation);
        }
        else
        {
            Instantiate(death, enemy.transform.position, enemy.transform.rotation);
        }


        gameObject.SetActive(false);
    }

    void Movement()
    {
        walkAnimation.transform.position = enemy.transform.position;
        walkAnimation.transform.rotation = enemy.transform.rotation;
        enemyRb.transform.rotation = collisionDetector.transform.rotation;

        float distance = Vector2.Distance(player.transform.position, enemyRb.transform.position);
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        rayToPlayer = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(direction.x, direction.y), distance);
        Debug.DrawRay(transform.position, direction, Color.red);
        Vector3 f = transform.TransformDirection(Vector3.up);
        RaycastHit2D obstacleCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(f.x, f.y), wallDetectionDistance);

        if (obstacleCheck.collider != null)
        {
            if (obstacleCheck.collider.gameObject.CompareTag("Wall"))
            {
                StartCoroutine("BackToPatrol");
                randomDirection = Random.Range(1, 4);

                if (randomDirection == 1)
                {
                    transform.Rotate(0, 0, 50);
                }
                else if (randomDirection == 3)
                {
                    transform.Rotate(0, 0, -50);
                }
                else if (randomDirection == 2)
                {
                    transform.Rotate(0, 0, 180);
                }
                else
                {
                    transform.Rotate(0, 0, 180);
                }
            }
        }

        if (moving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(!backToPatrol)
        {
            if (pursuing && playerLastPosition != null)
            {
                //speed = 0.8f;
                transform.up = player.transform.position - transform.position;

                if(player != null && rayToPlayer.collider != null)
                {
                    if (rayToPlayer.collider.gameObject.CompareTag("Player"))
                    {
                        playerLastPosition = player.transform.position;
                    }
                }
            }

            if (goingtolastloc)
            {
                //speed = 1f;
                transform.up = player.transform.position - transform.position;

                if (Vector3.Distance(transform.position, playerLastPosition) < distanceFromLastLocation)
                {
                    patrolling = true;
                    goingtolastloc = false;
                }
            }
        }

        if (patrolling)
        {
            //speed = 0.4f;

        }
    }

    void PlayerDetect()
    {

        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        //   Debug.Log("POS: "+ pos + "distance: " + Vector3.Distance(transform.position, player.transform.position));
        if (rayToPlayer.collider != null)
        {


            if (rayToPlayer.collider.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < playerDetectionDistance)
            {
                playerDetected = true;
                patrolling = false;
                pursuing = true;
                goingtoweapon = false;
            }

            else
            {
                playerDetected = false;

                if (pursuing)
                {
                    goingtolastloc = true;
                    pursuing = false;
                    goingtoweapon = false;
                }
            }
        }
    }

    IEnumerator BackToPatrol()
    {
        playerDetected = false;
        backToPatrol = true;
        yield return new WaitForSeconds(0.5f);
        backToPatrol = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Bullet") && dying == false)
        {
            dying = true;
            KilledByBullet();
        }
    }
}
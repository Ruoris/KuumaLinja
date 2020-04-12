using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player, enemy,aseDroppisjiainti,pistolDrop;
    Rigidbody2D enemyRb;
    public int ammoCapacity, ammoLeft;
    public Vector3 playerLastPosition;
    RaycastHit2D rayToPlayer;
    float speed = 3f;
    float detectionDistance = 10f;

    bool moving = true, patrolling = true, pursuing = false, hasGun = false, goingtoweapon = false, goingtolastloc = false;
    public bool clockwise = false, stationary = false;
    public int enemyType = 1;

    public GameObject walkAnimation;

    void Start()
    {
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
        walkAnimation.SetActive(true);
    }

    void KilledByBullet()
    {
   
        GetComponent<EnemyWeapons>().DropGun();
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
        walkAnimation.transform.position = enemy.transform.position;
        walkAnimation.transform.rotation = enemy.transform.rotation;

        float distance = Vector2.Distance(player.transform.position, enemyRb.transform.position);
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        rayToPlayer = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(direction.x, direction.y), distance);
        Debug.DrawRay(transform.position, direction, Color.red);
        Vector3 f = transform.TransformDirection(Vector3.up);
        RaycastHit2D obstacleCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(f.x, f.y), 1.2f);
        

        if (moving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (pursuing)
        {
            speed = 3.0f;
            enemyRb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPosition.y - transform.position.y), 
                (playerLastPosition.x - transform.position.x)) * Mathf.Rad2Deg);

            if (rayToPlayer.collider.gameObject.CompareTag("Player"))
            {
                playerLastPosition = player.transform.position;
                Debug.Log("seuraa");


                //if ( GetComponent<EnemyPshoot>().fireRate < canFire && !emptyMagazine)
                //{
                //    gunSound.Play();
                //    Fire();
                //    gunFlareAnimation.SetActive(true);

                //    GetComponent<Weapons>().ammoLeft--;

                //    if (equippedGun == 2)
                //    {
                //        // if a shotgun is equipped
                //        Fire();
                //        Fire();
                //        Fire();
                //        Fire();
                //        Fire();
                //        Fire();
                //    }
                //}


            }
        }

        if (goingtolastloc)
        {
            speed = 2.5f;
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
     //   Debug.Log("POS: "+ pos + "distance: " + Vector3.Distance(transform.position, player.transform.position));
        if (rayToPlayer.collider != null)
          
        {

            //Debug.Log("hit collider hit: "+ rayToPlayer.collider.gameObject.ToString());
            if (rayToPlayer.collider.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position)<detectionDistance)
            {
                patrolling = false;
                pursuing = true;
                goingtoweapon = false;
                //Debug.Log("player detected");
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
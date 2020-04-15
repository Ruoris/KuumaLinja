using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player, enemy, aseDroppisjiainti, pistolDrop;
    public GameObject enemySprite, footPrints;

    Rigidbody2D enemyRb;
    public int ammoCapacity, ammoLeft;
    public Vector3 playerLastPosition;
    RaycastHit2D rayToPlayer;
    public float speed = 0.4f;
    float wallDetectionDistance = 0.2f;
    float playerDetectionDistance = 2f;
    float distanceFromLastLocation = 1f; //kuinka pitkälle vihollinen jahtaa pelaajaa

    bool moving = true, patrolling = true, pursuing = false, hasGun = false, goingtoweapon = false, goingtolastloc = false,dying;
    public bool clockwise = false, stationary = false;


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
        if (gameObject.GetComponent<SpriteRenderer>().enabled == true && Time.time > 1)
        {
            footPrints.SetActive(true);
            enemySprite.SetActive(true);
            footPrints.transform.position = enemy.transform.position;
            footPrints.transform.rotation = enemy.transform.rotation;
        }
        else
        {
            enemySprite.SetActive(false);
            footPrints.transform.parent = null;
        }
    }

    void KilledByBullet()
    {
           GetComponent<EnemyWeapons>().DropGun();
           Destroy(gameObject);

    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.gameObject.CompareTag("Bullet")&&dying==false)
        {
            dying = true;
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
        RaycastHit2D obstacleCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(f.x, f.y), wallDetectionDistance);


        if (moving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (pursuing)
        {
            speed = 0.8f;
            transform.up = player.transform.position - transform.position;

            if (rayToPlayer.collider.gameObject.CompareTag("Player"))
            {
                playerLastPosition = player.transform.position;
                Debug.Log("seuraa");

            }
        }

        if (goingtolastloc)
        {
            speed = 0.6f;
            transform.up = player.transform.position - transform.position;

            if (Vector3.Distance(transform.position, playerLastPosition) < distanceFromLastLocation)
            {
                patrolling = true;
                goingtolastloc = false;
            }
        }

        if (patrolling)
        {
            speed = 0.4f;

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
            if (rayToPlayer.collider.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < playerDetectionDistance)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Wall"))
        {
            speed = -0.5f;
        }
    }
}
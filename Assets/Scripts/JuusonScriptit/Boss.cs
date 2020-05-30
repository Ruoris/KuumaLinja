using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D enemyRb;
    public RaycastHit2D rayToPlayer;
    float playerDetectionDistance = 10f;
    public GameObject fireStrike, blob;
    public GameObject boss;
    public GameObject lastDialogue;
    private float fireRate = 3f;
    private float canFire;
    public int health = 5;
    public GameObject locker;

    void Update()
    {
        Look();
        PlayerDetect();
    }

    IEnumerator FireStrike()
    {
        yield return new WaitForSeconds(1.95f);
        GameObject fireStrike2 = GameObject.Find("FireStrikeAnimation(Clone)");
        Destroy(fireStrike2);
    }

    void PlayerDetect()
    {
        float randX = Random.Range(0f, 6.36f);
        float randY = Random.Range(0f, 5f);

        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        if (rayToPlayer.collider != null)

        {
            if (rayToPlayer.collider.CompareTag("Player") && Vector3.Distance(transform.position, player.transform.position) < playerDetectionDistance)
            {
                float distance = Vector2.Distance(player.transform.position, enemyRb.transform.position);

                Vector2 direction2 = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

                transform.up = direction2;
                if (fireRate < canFire)
                {
                    canFire = 0;
                    Instantiate(fireStrike, enemyRb.transform.position, enemyRb.transform.rotation);
                    Instantiate(blob, player.transform.position + new Vector3(randX, randY, 0), Quaternion.identity);
                    StartCoroutine("FireStrike");
                }
                else
                {

                }
                canFire += Time.deltaTime;

            }
            else
            {
               
            }
            
        }
    }

    public void Look()
    {
        float distance = Vector2.Distance(player.transform.position, enemyRb.transform.position);
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        rayToPlayer = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(direction.x, direction.y), distance);
        Debug.DrawRay(transform.position, direction, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            if(health < 1)
            {   Destroy(locker) ;
                lastDialogue.SetActive(true);
                Destroy(boss);

            }
        }
    }
}

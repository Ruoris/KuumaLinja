using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public List<Transform> checkPoints = new List<Transform>();
<<<<<<< HEAD
    //public GameObject player;
=======
    public GameObject player;
>>>>>>> origin/TuomaksenBranch2
    public Rigidbody2D enemyRb;
    public int i = 0;
    public float speed = 0.4f;

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        transform.up = checkPoints[i].transform.position - transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            i++;
            if (i > 3)
            {
                i = 0;
            }
        }
    }
}

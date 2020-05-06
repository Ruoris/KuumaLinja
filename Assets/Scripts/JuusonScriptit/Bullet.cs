using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Untagged")
        {
            Physics2D.IgnoreCollision(other.collider, other.collider);
        }
        //if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);
        //}
    }
}

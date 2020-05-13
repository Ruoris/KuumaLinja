using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            //if(other.gameObject.CompareTag("Enemy"))
            //{
            //    var killed = other.gameObject.GetComponent<EnemyController>();
            //    killed.killed
            //}
        }
    }
}

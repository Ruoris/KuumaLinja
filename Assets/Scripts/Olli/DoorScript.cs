using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    //private Rigidbody2D r2bd;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<Rigidbody2D>();
        //r2bd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log("collision!");
//     if (collision.gameObject.tag == "Wall")
//        {
//            Debug.Log("Wall!");
//            Destroy(collision.gameObject);
//            if (r2bd != null)
//            {
//                Destroy(r2bd);
//            }
//        }   
//    }
}

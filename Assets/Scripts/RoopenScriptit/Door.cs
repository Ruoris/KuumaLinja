using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doortrigger;
    public bool open;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator OpenDoor()
    {
        doortrigger.transform.Rotate(0, 0, 90);
        yield return null;
        open = true;
        active = false;
    }
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(0.3f);
        doortrigger.transform.Rotate(0, 0, -90);
        open = false;
        active = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && open == false)
        {
            StartCoroutine("OpenDoor");
        }       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && open == true && active == false)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& open == true && active == false)
        {          
            StartCoroutine("CloseDoor");                     
        }
    }

}

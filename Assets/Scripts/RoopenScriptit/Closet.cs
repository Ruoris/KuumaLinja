using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Closet : MonoBehaviour
{
    public GameObject doortrigger;
    public bool open;
    public bool active;
    public float rotationOpen;
    public float rotationClose;
    private Transform playerTrans = null;
    
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        playerTrans = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Fire2")&& !active)
        {
            if(Vector3.Distance(playerTrans.position,this.transform.position) < 5f)
            {
                //SetPanelText();
                if (open)
                {
                    StartCoroutine("CloseDoor");
                }
                else
                {
                    StartCoroutine("OpenDoor");
                }
            }
        }
    }
    IEnumerator OpenDoor()
    {
        active = true;
        doortrigger.transform.Rotate(0, 0, rotationOpen);
        yield return null;
        open = true;
        active = false;
    }
    IEnumerator CloseDoor()
    {
        active = true;
        yield return null;
        doortrigger.transform.Rotate(0, 0, rotationClose);
        open = false;
        active = false;
    }
    private void SetPanelText()
    {

        doortrigger.GetComponent<Text>().text="F";
      
    }
}

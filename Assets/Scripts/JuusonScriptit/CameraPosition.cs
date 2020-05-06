using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;

    private void Start()
    {
        //Instantiate(playerCamera, player.transform.position + new Vector3(0, 0, -10), player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}

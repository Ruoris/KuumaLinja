//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraPosition : MonoBehaviour
//{
//    public GameObject player;
//    public GameObject playerCamera;

//    private void Start()
//    {
//        //Instantiate(playerCamera, player.transform.position + new Vector3(0, 0, -10), player.transform.rotation);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        var player = GameObject.FindWithTag("Player");
//        if(player != null)
//        {
//            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraPosition : MonoBehaviour
{
    public GameObject player;

    private float xMax, xMin, yMin, yMax;
    // Update is called once per frame

    void SetCameraValues(int xmax, int xmin, int ymin, int ymax) //kameran minimi/maksimi sallitut koordinaatit
    {
        xMax = xmax;
        xMin = xmin;
        yMin = ymin;
        yMax = ymax;
    }

    void Start()
    {
        SetCameraValues(10, 1, 1, 10);
    }
    void LateUpdate()
    {

        var player = GameObject.Find("Player");

        if (player != null)
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, xMin, xMax), Mathf.Clamp(player.transform.position.y, yMin, yMax), -10);
        }
    }
}

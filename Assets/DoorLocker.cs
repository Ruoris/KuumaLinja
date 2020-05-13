using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocker : MonoBehaviour
{
    public GameObject bossLass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossLass.GetComponent<Boss>().health < 0)
        {
            Destroy(this);
        }
        
    }
}

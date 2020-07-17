using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponDrop : MonoBehaviour
{
    public int bulletsLeft;
    public int ammoCapacity;
    private bool started;
    public float timer;
   
    public void Start()
    {
        timer = 0;
        started = true;
        //bulletsLeft = ammoCapacity;

    }
    [PunRPC]
   void OppenHeimer()
    {
        Destroy(this.gameObject);
    }
    
    public void RpcCaller()
    {   PhotonView photonview = PhotonView.Get(this); 
        photonview.RPC("OppenHeimer", RpcTarget.All);
    }
    public void FixedUpdate()

    {
        if (started)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                gameObject.layer = 11;
                started = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }

        }
    }
}

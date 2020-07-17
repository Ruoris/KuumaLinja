using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiWeaponDrop : MonoBehaviour
{
    public int bulletsLeft;
    public int ammoCapacity;
    private bool started;
    public float timer;
    public PhotonView photonview;
    public void Start()
    {
        timer = 0;
        started = true;
        //bulletsLeft = ammoCapacity;
        photonview = PhotonView.Get(this);
    }
    [PunRPC]
   void PickedUp()
    {
        Destroy(this.gameObject);
    }
    
    public void RpcCaller()
    {
        photonview.RPC("PickedUp", RpcTarget.All);
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

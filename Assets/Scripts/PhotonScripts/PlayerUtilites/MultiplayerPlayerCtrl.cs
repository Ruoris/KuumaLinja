using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MultiplayerPlayerCtrl : MonoBehaviourPun
{

    public float movementSpeed;
    public GameObject player, aim, playerCamera, startPoint;
    public GameObject walkAnimation, deathAnimation, bloodFrame;
    public int playerHealth = 1;
    public GameObject pauser;
    public bool crouching;

    public Rigidbody2D playerRB;

    Vector2 movement;

    public GameObject dialogueTrigger;
    public PhotonView photonview;

    void Start()
    {
         photonview = PhotonView.Get(this);
        
        if (base.photonView.IsMine)
        {
            //dialogue = false;

            Cursor.visible = false;
            pauser = GameObject.FindWithTag("soundsettings");
            player.SetActive(true);
            movementSpeed = 1.8f;

            Instantiate(aim, player.transform.position, player.transform.rotation);
            //Instantiate(playerCamera, player.transform.position + new Vector3(0,0,-10), player.transform.rotation);


            playerRB = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (base.photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerHealth--;
                Debug.Log("f painettu");
            }
            if (playerHealth <= 0)
            {
                player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z - 180);
                Instantiate(deathAnimation, player.transform.position, player.transform.rotation);
                GameObject deathPanel = GameObject.Find("/Misc stuff/Canvas/DeathPanel");
                deathPanel.SetActive(true);
                // pauser.GetComponent<Pause>().alive = false;
                Cursor.visible = true;
                player.SetActive(false);
            }
            //if (pauser.GetComponent<Pause>().paused == false){

            FaceMouse();
            Crouch();



            playerRB.MovePosition(playerRB.position + movement * movementSpeed * Time.fixedDeltaTime);
            FaceMouse();
            Crouch();

            //playerCamera.transform.position = player.transform.position + new Vector3(0, 0, -10);

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            if (movement.x != 0 || movement.y != 0)
            {
                walkAnimation.SetActive(true);
                bool walking = true;
                


                walkAnimation.transform.position = player.transform.position;

                float walkAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
                walkAnimation.transform.rotation = Quaternion.AngleAxis(-walkAngle, Vector3.forward);
                photonview.RPC("Walking", RpcTarget.Others,walking,walkAngle);
            }
            else
            {
                bool walking = false;
                float notwalking = 0;
                walkAnimation.SetActive(false);
                photonview.RPC("Walking", RpcTarget.Others, walking,notwalking);
            }

        }
    }

    [PunRPC]
   public void Walking(bool walking,float walkAngle)
    {
        walkAnimation.SetActive(walking);
        walkAnimation.transform.position = player.transform.position;
        if (walking)
        {
        walkAnimation.transform.rotation = Quaternion.AngleAxis(-walkAngle, Vector3.forward);
        }
       
    }
    void Crouch()
    {
        if (base.photonView.IsMine)
        {
            if (Input.GetButtonDown("Crouch") && !crouching)
            {
                crouching = true;
                movementSpeed = 1.0f;
            }
            else if (Input.GetButtonDown("Crouch") && crouching)
            {
                crouching = false;
                movementSpeed = 1.8f;
            }
        }
    }

    void FaceMouse()
    {
        if (base.photonView.IsMine)
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            transform.up = direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (base.photonView.IsMine)
        {
            if (other.collider.CompareTag("EnemyBullet"))
            {
                playerHealth--;
                if (playerHealth < 1)

                {
                    player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z - 180);
                    Instantiate(deathAnimation, player.transform.position, player.transform.rotation);
                    Instantiate(bloodFrame, player.transform.position, player.transform.rotation);
                   // GameObject deathPanel = GameObject.Find("/Misc stuff/Canvas/DeathPanel");
                    //deathPanel.SetActive(true);
                    // pauser.GetComponent<Pause>().alive = false;
                    Cursor.visible = true;
                    player.SetActive(false);
                }
            }
        }
    }
}

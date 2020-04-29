using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    private float movementSpeed;
    public GameObject player, aim, playerCamera;
    public GameObject walkAnimation, deathAnimation;
    public int playerHealth = 1;
    public GameObject pauser;
    public bool crouching;

    public Rigidbody2D playerRB;

    Vector2 movement;

    void Start()
    {
        Cursor.visible = false;
        pauser = GameObject.FindWithTag("soundsettings");
        player.SetActive(true);
        movementSpeed = 1.8f;
        playerCamera = GameObject.FindWithTag("MainCamera");
        // lisätään kunhan saadaan playerin prefab "valmiiksi"
        //Instantiate(PlayerPrefab, startPoint.transform.position, Quaternion.identity);

        playerRB = GetComponent<Rigidbody2D>();
    }
   
    void Update()
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
            pauser.GetComponent<Pause>().alive = false;
            Cursor.visible = true;
            player.SetActive(false);
        }
        if (pauser.GetComponent<Pause>().paused == false)
        { 
            FaceMouse();
            Crouch();
            //var walkAnimation = GetComponent<Animator>();
            //var idle = GetComponent<SpriteRenderer>();

            playerCamera.transform.position = player.transform.position + new Vector3(0, 0, -10);

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            if (movement.x != 0 || movement.y != 0)
            {
                walkAnimation.SetActive(true);

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

                //walkAnimation.enabled = true;

                walkAnimation.transform.position = player.transform.position;


                float walkAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
                walkAnimation.transform.rotation = Quaternion.AngleAxis(-walkAngle, Vector3.forward);
            }
            else
            {
                walkAnimation.SetActive(false);
                //walkAnimation.enabled = false;
                
            }
        }
    }

    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch") && !crouching)
        {
            crouching = true;
            movementSpeed = 1.8f;
        }
        else if (Input.GetButtonDown("Crouch") && crouching)
        {
            crouching = false;
            movementSpeed = 1.0f;
        }
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        aim.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        //if(mousePosition.x < player.transform.position.y && movement.x != 0 || movement.y != 0)
        //{
        //    reverseWalk();
        //}

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    //public void reverseWalk()
    //{
    //    Debug.Log("ASD");
    //    float angle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
    //    walkAnimation.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //} // ei toimi vielä

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            playerHealth--;
            if (playerHealth <= 0)
            {
                player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z - 180);
                Instantiate(deathAnimation, player.transform.position, player.transform.rotation);
                GameObject deathPanel  = GameObject.Find("/Misc stuff/Canvas/DeathPanel");
                deathPanel.SetActive(true);
                pauser.GetComponent<Pause>().alive = false;
                Cursor.visible = true;
                player.SetActive(false);
            }
            Destroy(other.gameObject);
        }
    }
}

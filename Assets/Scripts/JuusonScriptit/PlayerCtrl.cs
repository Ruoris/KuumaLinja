using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    private float movementSpeed;
    public GameObject player, aim, playerCamera, startPoint;
    public GameObject walkAnimation, deathAnimation, bloodFrame;
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

        Vector3 campPos = player.transform.position + new Vector3(0, 0, -10);
        Instantiate(aim, player.transform.position, player.transform.rotation);
        //Instantiate(playerCamera, campPos, Quaternion.identity);

        // lisätään kunhan saadaan playerin prefab "valmiiksi"
        //Instantiate(player, startPoint.transform.position, Quaternion.identity);

        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (pauser.GetComponent<Pause>().paused == false)
        {
            FaceMouse();
            Crouch();

            playerCamera.transform.position = player.transform.position + new Vector3(0, 0, -10);

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            if (movement.x != 0 || movement.y != 0)
            {
                walkAnimation.SetActive(true);



                walkAnimation.transform.position = player.transform.position;

                float walkAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
                walkAnimation.transform.rotation = Quaternion.AngleAxis(-walkAngle, Vector3.forward);
            }
            else
            {
                walkAnimation.SetActive(false);
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

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.collider.name);
        if (other.collider.CompareTag("EnemyBullet"))
        {

            playerHealth--;
            if(playerHealth < 1)
            {
                player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z - 180);
                Instantiate(deathAnimation, player.transform.position, player.transform.rotation);
                Instantiate(bloodFrame, player.transform.position, player.transform.rotation);


                player.SetActive(false);
            }
        }
    }
}
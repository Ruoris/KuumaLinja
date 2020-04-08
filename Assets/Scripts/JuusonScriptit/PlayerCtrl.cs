using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    public GameObject player, aim, playerCamera;
    public GameObject walkAnimation, deathAnimation;
    private int playerHealth = 1;

    public bool crouching;

    public Rigidbody2D playerRB;

    Vector2 movement;

    void Start()
    {
        player.SetActive(true);

        // lisätään kunhan saadaan playerin prefab "valmiiksi"
        //Instantiate(PlayerPrefab, startPoint.transform.position, Quaternion.identity);

        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
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

    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * movSpeed * Time.fixedDeltaTime);
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch") && !crouching)
        {
            crouching = true;
            movSpeed = 2;
        }
        else if (Input.GetButtonDown("Crouch") && crouching)
        {
            crouching = false;
            movSpeed = 4;
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

    public void reverseWalk()
    {
        Debug.Log("ASD");
        float angle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
        walkAnimation.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    } // ei toimi vielä

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            playerHealth--;
            if(playerHealth <= 0)
            {
                player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z - 180);
                Instantiate(deathAnimation, player.transform.position, player.transform.rotation);

                player.SetActive(false);
            }
        }
    }
}
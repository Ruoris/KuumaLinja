using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    public GameObject player, aim, walkAnimation;

    public bool crouching;
    private float timer = 0.3f;
    private float crouchTimer;

    public Rigidbody2D playerRB;

    Vector2 movement;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouse();
        Crouch();

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
        crouchTimer = crouchTimer + Time.deltaTime;

        if (Input.GetButton("Crouch") && !crouching && timer < crouchTimer)
        {
            crouchTimer = 0;
            crouching = true;
            movSpeed = 2;
        }
        if (Input.GetButton("Crouch") && crouching && timer < crouchTimer)
        {
            crouchTimer = 0;
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
    }
}

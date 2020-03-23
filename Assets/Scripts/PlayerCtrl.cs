using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    public GameObject aim;
    public bool crouching;
    private float timer = 0.3f;
    private float crouchTimer;

    public Rigidbody2D plaRB;
    //public Animator animator;

    Vector2 movement;

    void Start()
    {
        plaRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouse();
        Crouch();
        crouchTimer = crouchTimer + Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        plaRB.MovePosition(plaRB.position + movement * movSpeed * Time.fixedDeltaTime);
    }

    void Crouch()
    {
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

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}

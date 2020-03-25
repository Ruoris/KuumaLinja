using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    public GameObject player, aim, pistol, shotgun, assaultRifle;
    public int equippedGun;
    public bool crouching;
    private float timer = 0.3f;
    private float crouchTimer;

    public Rigidbody2D plaRB;
    public Animation walkAnim;
    //public Animator animator;

    Vector2 movement;

    void Start()
    {
        equippedGun = 0;
        plaRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouse();
        Crouch();
        EquipGun();

        crouchTimer = crouchTimer + Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        plaRB.MovePosition(plaRB.position + movement * movSpeed * Time.fixedDeltaTime);
        //walkAnim.Play();
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

    void EquipGun()
    {
        switch (equippedGun)
        {
            case 1:
                pistol.SetActive(true);
                break;
            case 2:
                shotgun.SetActive(true);
                break;
            case 3:
                assaultRifle.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        if(other.gameObject.tag == "PistolBox")
        {
            equippedGun = 1;
        }
        if (other.gameObject.tag == "ShotgunBox")
        {
            equippedGun = 2;
        }

        if (other.gameObject.tag == "Arbox")
        {
            equippedGun = 3;
        }
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        aim.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}

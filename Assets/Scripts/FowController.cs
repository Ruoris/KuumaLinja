using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowController : MonoBehaviour
{
    [SerializeField]
    private GameObject roomController;
    public SpriteRenderer sprite;

    public bool pInRoom;



    // Start is called before the first frame update
    void Start()
    {
        roomController = GameObject.FindGameObjectWithTag("RoomController");
    }

    // Update is called once per frame
    void Update()
    {
        if (roomController.GetComponent<RoomGenerator>().playerOnTile > 0)
        {
            pInRoom = true;
            sprite.color = new Color(1f, 1f, 1f, 0);
        }
        if (roomController.GetComponent<RoomGenerator>().playerOnTile == 0)
        {
            pInRoom = false;
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            roomController.GetComponent<RoomGenerator>().playerOnTile++;
            pInRoom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        roomController.GetComponent<RoomGenerator>().playerOnTile--;
        pInRoom = false;
    }
}

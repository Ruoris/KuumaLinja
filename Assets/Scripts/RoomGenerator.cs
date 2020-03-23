using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public float roomX;
    public float roomY;

    public int connections;

    public GameObject floor;
    public GameObject wallTop;
    public GameObject wallBot;
    public GameObject wallRight;
    public GameObject wallLeft;
    public GameObject fogTile;

    public GameObject player;

    private Vector2 v2;

    public float playerOnTile;

    // Start is called before the first frame update
    void Start()
    {
        v2 = transform.position;
        Vector2 v2Orig = v2;
        float _x = 0;
        float _y = 0;
        playerOnTile = 0;

        //GameObject fow = new GameObject("FogOfWar");
        //GameObject walls = new GameObject("Walls");
        //GameObject floor = new GameObject("Floor");
        //fow.transform.parent = this.transform;
        //walls.transform.parent = this.transform;
        //floor.transform.parent = this.transform;

        while (roomY > _y)
        {
            while (roomX > _x)
            {
                v2.x = v2.x + 0.64f;
                GameObject floors = Instantiate(floor, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                GameObject fogs = Instantiate(fogTile, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                //fogs.transform.parent = fow.transform;
                //floors.transform.parent = floor.transform;
                _x++;
                if (_y == 0)
                {
                    GameObject botWalls = Instantiate(wallBot, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    //botWalls.transform.parent = walls.transform;
                }
                if (_y == roomY - 1)
                {
                    GameObject topWalls = Instantiate(wallTop, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    //topWalls.transform.parent = walls.transform;
                }
                if (_x == 1)
                {
                    GameObject leftWalls = Instantiate(wallLeft, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    //leftWalls.transform.parent = walls.transform;
                }
                if (_x == roomX)
                {
                    GameObject rightWalls = Instantiate(wallRight, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    //rightWalls.transform.parent = walls.transform;
                }
            }

            if (_x == roomX && _y < roomY)
            {
                _x = 0;
                v2.x = v2Orig.x;
                v2.y = v2.y + 0.64f;
                _y++;
            }
        }
        GameObject _player = Instantiate(player, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnTile < 0)
        {
            playerOnTile = 0;
            Debug.Log("ERROR!");
        }
    }
}

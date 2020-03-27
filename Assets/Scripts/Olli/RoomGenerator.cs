using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int connections;

    public Room[] rooms;

    public GameObject fogTile;
    public GameObject player;
    public GameObject aim;

    public int[] doors;
    private int doorNum = 0;

    private Vector2 v2;

    public float playerOnTile;

    // Start is called before the first frame update
    void Start()
    {
        playerOnTile = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateRoom(Room room)
    {
        Vector2 v2Orig = v2;
        float _x = 0;
        float _y = 0;

        while (room.roomY > _y)
        {
            int doorcounter = 0;
            while (room.roomX > _x)
            {
                v2.x = v2.x + 0.64f;
                GameObject floors = Instantiate(room.floor, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                doorcounter++;
                _x++;
                if (_y == 0)
                {
                    if (doorNum < doors.Length && doorcounter == doors[doorNum])
                    {
                        doorNum++;
                        GameObject _door = Instantiate(room.door, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    }
                    else
                    {
                        GameObject botWalls = Instantiate(room.wall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    }
                }
                if (_y == room.roomY - 1)
                {
                    if (doorNum < doors.Length && doorcounter == doors[doorNum])
                    {
                        doorNum++;
                        GameObject _door = Instantiate(room.door, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                    }
                    else
                    {
                        GameObject topWalls = Instantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                    }
                }
                if (_x == 1)
                {
                    if (doorNum < doors.Length && doorcounter == doors[doorNum])
                    {
                        doorNum++;
                        GameObject _door = Instantiate(room.door, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                    }
                    else
                    {
                        GameObject leftWalls = Instantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                    }
                }
                if (_x == room.roomX)
                {
                    if (doorNum < doors.Length && doorcounter == doors[doorNum])
                    {
                        doorNum++;
                        GameObject _door = Instantiate(room.door, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                    }
                    else
                    {
                        GameObject rightWalls = Instantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                    }
                }
            }

            if (_x == room.roomX && _y < room.roomY)
            {
                _x = 0;
                v2.x = v2Orig.x;
                v2.y = v2.y + 0.64f;
                _y++;
            }
        }
    }
}

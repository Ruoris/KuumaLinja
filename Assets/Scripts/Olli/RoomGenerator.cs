using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int connections;

    private Vector2 v2;

    public float playerOnTile;

    private GameObject newRoom;

    // Start is called before the first frame update
    void Start()
    {
        playerOnTile = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public GameObject GenerateRoom(Room room, Vector2 vec2, int rX, int rY, string doors, string windows, GameObject _parent)
    {
        v2 = vec2;
        Vector2 v2Orig = v2;
        int _x = 0;
        int _y = 0;
        newRoom = Instantiate(new GameObject("Room"), v2, transform.rotation = new Quaternion(0, 0, 0, 0));

        bool[] forbiddenX = new bool[rX];
        bool[] forbiddenY = new bool[rY];

        int spawnXint = 0;
        int spawnYint = 0;

        int nextDoor = 0;

        for (int x = 0; x < doors.Length; x++)
        {
            if (doors[x] == '-')
            {
                int save = x;
                x = nextDoor;
                string spawnString = null;
                while (x < save)
                {
                    spawnString += doors[x];
                    x++;
                }
                int.TryParse(spawnString, out spawnXint);
                x++;
                spawnString = null;
                while (x < doors.Length)
                {
                    spawnString += doors[x];
                    int.TryParse(spawnString, out spawnYint);
                    if (doors[x] == ',')
                    {
                        nextDoor = x + 1;
                        break;
                    }
                    x++;
                }
                forbiddenX[spawnXint] = true;
                forbiddenY[spawnYint] = true;
            }
        }

        while (rY > _y)
        {

            while (rX > _x)
            {
                v2.x = v2.x + 0.32f;
                GameObject floors = Instantiate(room.floor, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                floors.transform.parent = newRoom.transform;


                if (_y == 0)
                {
                    if ((_x == forbiddenX.Length-1 && _y == 0) || (_x == 0 && _y== 0))
                    {
                        GameObject botWalls = Instantiate(room.wall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                        botWalls.transform.parent = newRoom.transform;
                    }
                    else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                    {
                        GameObject door = Instantiate(room.door, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                        door.transform.parent = newRoom.transform;
                    }
                    else
                    {
                        GameObject botWalls = Instantiate(room.wall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                        botWalls.transform.parent = newRoom.transform;
                    }
                }
                if (_y == rY - 1)
                {
                    Vector2 v2Temp = v2;
                    v2Temp.y = v2.y;
                    if ((_x == forbiddenX.Length - 1 && _y == forbiddenY.Length - 1) || (_x == 0 && _y == forbiddenY.Length - 1))
                    {
                        GameObject topWalls = Instantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        topWalls.transform.parent = newRoom.transform;
                    }
                    else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                    {
                        GameObject door = Instantiate(room.door, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        door.transform.parent = newRoom.transform;
                    }
                    else
                    {
                        GameObject topWalls = Instantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        topWalls.transform.parent = newRoom.transform;
                    }
                }
                if (_x == 0)
                {
                    Vector2 v2Temp = v2;
                    v2Temp.x = v2.x;

                    if ((_y == forbiddenY.Length - 1 && _x == 0) || (_x == 0 && _y == 0))
                    {
                        GameObject leftWalls = Instantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        leftWalls.transform.parent = newRoom.transform;
                    }
                    else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                    {
                        GameObject door = Instantiate(room.door, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        door.transform.parent = newRoom.transform;
                    }
                    else
                    {
                        GameObject leftWalls = Instantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        leftWalls.transform.parent = newRoom.transform;
                    }
                }
                if (_x == rX - 1)
                {
                    if ((_x == forbiddenX.Length-1 && _y == 0) || (_x == forbiddenX.Length - 1 && _y == forbiddenY.Length - 1))
                    {
                        GameObject rightWalls = Instantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        rightWalls.transform.parent = newRoom.transform;
                    }
                    else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                    {
                        GameObject door = Instantiate(room.door, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        door.transform.parent = newRoom.transform;
                    }
                    else
                    {
                        GameObject rightWalls = Instantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        rightWalls.transform.parent = newRoom.transform;
                    }
                }
                _x++;
                if (_x == rX && _y < rY)
                {
                    _x = 0;
                    v2.x = v2Orig.x;
                    v2.y = v2.y + 0.32f;
                    _y++;
                }

                if (_y == forbiddenY.Length)
                {
                    break;
                }
            }
        }
        Vector2 roomPos = newRoom.transform.position;
        newRoom.transform.parent = _parent.transform;

        float roomPosY = roomPos.x;
        float roomPosX = roomPos.y;
        roomPos.x = roomPosX;
        roomPos.y = roomPosY;

        //if (newRoom.transform.position.y < 0.1)
        //{
        //    Vector2 fix = newRoom.transform.position;
        //    fix.y = 0;
        //    newRoom.transform.position = fix;
        //}

        //if (newRoom.transform.position.x < 0.1)
        //{
        //    Vector2 fix = newRoom.transform.position;
        //    fix.x = 0;
        //    newRoom.transform.position = fix;
        //}

        //transform.rotation = new Quaternion(0, 0, 0, 0);
        //roomPos.y = roomPos.y * -1;

        newRoom.transform.position = roomPos;

        return newRoom;
    }
    public void GenerateFurniture(string layouts, Vector2 vec2, Room room, GameObject _parent)
    {
        int spawnXint = 0;
        int spawnYint = 0;
        Vector2 vectorOrig = vec2;
        int nextC = 0;
        GameObject objectToGenerate = room.furniture[0];

        for (int x = 0; x < layouts.Length; x++)
        {
            if (layouts[x] == ' ')
            {
                x++;
            }
            if (layouts[x] == 'a')
            {
                objectToGenerate = room.furniture[0];
                x++;
                nextC = x;
            }
            else if (layouts[x] == 'b')
            {
                objectToGenerate = room.furniture[1];
                x++;
                nextC = x;
            }
            else if (layouts[x] == 'c')
            {
                objectToGenerate = room.furniture[2];
                x++;
                nextC = x;
            }
            else if (layouts[x] == 'd')
            {
                objectToGenerate = room.furniture[3];
                x++;
                nextC = x;
            }
            else if (layouts[x] == 'e')
            {
                objectToGenerate = room.furniture[4];
                x++;
                nextC = x;
            }
            else if (layouts[x] == 'f')
            {
                objectToGenerate = room.furniture[5];
                x++;
                nextC = x;
            }
            if (layouts[x] == '-')
            {
                int save = x;
                x = nextC;
                string spawnString = null;
                while (x < save)
                {
                    spawnString += layouts[x];
                    x++;
                }
                int.TryParse(spawnString, out spawnXint);
                x++;
                spawnString = null;
                while (x < layouts.Length)
                {
                    spawnString += layouts[x];
                    if (layouts[x] == ',')
                    {
                        break;
                    }
                    int.TryParse(spawnString, out spawnYint);
                    x++;
                }
                nextC = x;
                vec2.x += 0.32f * spawnXint;
                vec2.y += 0.32f * spawnYint;
                GameObject _object = Instantiate(objectToGenerate, vec2, transform.rotation = new Quaternion(0, 0, 0, 0));
                vec2 = vectorOrig;
                _object.transform.parent = _parent.transform;
                //vec2 = _parent.transform.position;
                //vec2.x += spawnXint * 0.32f;
                //vec2.y += spawnYint * 0.32f;
                //_object.transform.position = vec2;
            }
        }
    }
}
            
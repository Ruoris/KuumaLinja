﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorMultiplayer : MonoBehaviour
{
    public int connections;

    private Vector2 v2;

    private GameObject newRoom;

    // Start is called before the first frame update
    
    public GameObject GenerateRoom(Room room, Vector2 vec2, int rX, int rY, string doors, GameObject _parent)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            v2 = vec2;
            Vector2 v2Orig = v2;
            int _x = 0;
            int _y = 0;
            bool doorPlacedLeft = false;
            bool doorPlacedRight = false;
            bool doorPlacedTop = false;
            bool doorPlacedBot = false;
            newRoom = MasterManager.NetworkInstantiate(new GameObject("Room"), v2, transform.rotation = new Quaternion(0, 0, 0, 0));

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
                    GameObject floors = MasterManager.NetworkInstantiate(room.floor, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                    floors.transform.parent = newRoom.transform;

                    //CornerGenerator
                    if (_x == 0 && _y == 0)
                    {
                        GameObject downLeftCorner = MasterManager.NetworkInstantiate(room.wallCorner, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                        downLeftCorner.transform.parent = newRoom.transform;
                    }

                    if (_x == forbiddenX.Length - 1 && _y == 0)
                    {
                        GameObject downRightCorner = MasterManager.NetworkInstantiate(room.wallCorner, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        downRightCorner.transform.parent = newRoom.transform;
                    }

                    if (_x == 0 && _y == forbiddenY.Length - 1)
                    {
                        GameObject topLeftCorner = MasterManager.NetworkInstantiate(room.wallCorner, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        topLeftCorner.transform.parent = newRoom.transform;
                    }

                    if (_x == forbiddenX.Length - 1 && _y == forbiddenY.Length - 1)
                    {
                        GameObject topRightCorner = MasterManager.NetworkInstantiate(room.wallCorner, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        topRightCorner.transform.parent = newRoom.transform;
                    }

                    //Bot Wall Generator
                    if (_y == 0)
                    {
                        if ((_x == forbiddenX.Length - 1 && _y == 0) || (_x == 0 && _y == 0))
                        {
                            if (doorPlacedBot == true)
                            {
                                GameObject botHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                                botHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedBot = false;
                            }
                            else
                            {
                                GameObject botWalls = MasterManager.NetworkInstantiate(room.wall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                                botWalls.transform.parent = newRoom.transform;
                            }

                        }
                        else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                        {
                            GameObject door = MasterManager.NetworkInstantiate(room.door, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                            door.transform.parent = newRoom.transform;
                            doorPlacedBot = true;
                        }
                        else
                        {
                            if (doorPlacedBot == true)
                            {
                                GameObject botHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                                botHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedBot = false;
                            }
                            else
                            {
                                GameObject botWalls = MasterManager.NetworkInstantiate(room.wall, v2, transform.rotation = new Quaternion(0, 0, 0, 0));
                                botWalls.transform.parent = newRoom.transform;
                            }
                        }
                    }

                    //Top Walls Generator
                    if (_y == rY - 1)
                    {
                        Vector2 v2Temp = v2;
                        v2Temp.y = v2.y;

                        Vector2 doorLocalScale;

                        if ((_x == forbiddenX.Length - 1 && _y == forbiddenY.Length - 1) || (_x == 0 && _y == forbiddenY.Length - 1))
                        {
                            if (doorPlacedTop == true)
                            {
                                GameObject topHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                                doorLocalScale = topHalfWalls.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                topHalfWalls.transform.localScale = doorLocalScale;
                                topHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedTop = false;
                            }
                            else
                            {
                                GameObject topWalls = MasterManager.NetworkInstantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                                topWalls.transform.parent = newRoom.transform;
                            }
                        }
                        else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                        {
                            GameObject door = MasterManager.NetworkInstantiate(room.door, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                            doorLocalScale = door.transform.localScale;
                            doorLocalScale.x = doorLocalScale.x * -1;
                            door.transform.localScale = doorLocalScale;
                            door.transform.parent = newRoom.transform;
                            doorPlacedTop = true;
                        }
                        else
                        {
                            if (doorPlacedTop == true)
                            {
                                GameObject topHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                                doorLocalScale = topHalfWalls.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                topHalfWalls.transform.localScale = doorLocalScale;
                                topHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedTop = false;
                            }
                            else
                            {
                                GameObject topWalls = MasterManager.NetworkInstantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                                topWalls.transform.parent = newRoom.transform;
                            }
                        }
                    }

                    //Left Wall Generator
                    if (_x == 0)
                    {
                        Vector2 v2Temp = v2;
                        v2Temp.x = v2.x;
                        Vector2 doorLocalScale;

                        if ((_y == forbiddenY.Length - 1 && _x == 0) || (_x == 0 && _y == 0))
                        {
                            if (doorPlacedLeft == true)
                            {
                                GameObject leftHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                doorLocalScale = leftHalfWalls.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                leftHalfWalls.transform.localScale = doorLocalScale;
                                leftHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedLeft = false;
                            }
                            else
                            {
                                GameObject leftWalls = MasterManager.NetworkInstantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                leftWalls.transform.parent = newRoom.transform;
                            }
                        }
                        else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                        {
                            if (doorPlacedLeft == true)
                            {
                                GameObject leftHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                doorLocalScale = leftHalfWalls.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                leftHalfWalls.transform.localScale = doorLocalScale;
                                leftHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedLeft = false;
                                Debug.Log("HalfWallELeft");
                            }
                            else
                            {
                                GameObject door = MasterManager.NetworkInstantiate(room.door, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                doorLocalScale = door.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                door.transform.localScale = doorLocalScale;
                                door.transform.parent = newRoom.transform;
                                doorPlacedLeft = true;
                            }
                        }
                        else
                        {
                            if (doorPlacedLeft == true)
                            {
                                GameObject leftHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                doorLocalScale = leftHalfWalls.transform.localScale;
                                doorLocalScale.x = doorLocalScale.x * -1;
                                leftHalfWalls.transform.localScale = doorLocalScale;
                                leftHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedLeft = false;
                            }
                            else
                            {
                                GameObject leftWalls = MasterManager.NetworkInstantiate(room.wall, v2Temp, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                                leftWalls.transform.parent = newRoom.transform;
                            }
                        }
                    }

                    //Right Wall Generator
                    if (_x == rX - 1)
                    {
                        if ((_x == forbiddenX.Length - 1 && _y == 0) || (_x == forbiddenX.Length - 1 && _y == forbiddenY.Length - 1))
                        {
                            if (doorPlacedRight == true)
                            {
                                GameObject rightHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                                rightHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedRight = false;
                            }
                            else
                            {
                                GameObject rightWalls = MasterManager.NetworkInstantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                                rightWalls.transform.parent = newRoom.transform;
                            }
                        }
                        else if (forbiddenX[_x] == true && forbiddenY[_y] == true)
                        {
                            GameObject door = MasterManager.NetworkInstantiate(room.door, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                            door.transform.parent = newRoom.transform;
                            doorPlacedRight = true;
                        }
                        else
                        {
                            if (doorPlacedRight == true)
                            {
                                GameObject rightHalfWalls = MasterManager.NetworkInstantiate(room.halfWall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                                rightHalfWalls.transform.parent = newRoom.transform;
                                doorPlacedRight = false;
                            }
                            else
                            {
                                GameObject rightWalls = MasterManager.NetworkInstantiate(room.wall, v2, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                                rightWalls.transform.parent = newRoom.transform;
                            }
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
        else { return null; }
    }
        public void GenerateFurniture(string layouts, Vector2 vec2, Room room, GameObject _parent)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int spawnXint = 0;
                int spawnYint = 0;
                Vector2 vectorOrig = vec2;
                int nextC = 0;
                GameObject objectToGenerate = room.furniture[0];
                if (layouts != null)
                {
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
                        else if (layouts[x] == 'g')
                        {
                            objectToGenerate = room.furniture[6];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'h')
                        {
                            objectToGenerate = room.furniture[7];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'i')
                        {
                            objectToGenerate = room.furniture[8];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'j')
                        {
                            objectToGenerate = room.furniture[9];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'k')
                        {
                            objectToGenerate = room.furniture[10];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'l')
                        {
                            objectToGenerate = room.furniture[11];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'm')
                        {
                            objectToGenerate = room.furniture[12];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'n')
                        {
                            objectToGenerate = room.furniture[13];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'o')
                        {
                            objectToGenerate = room.furniture[14];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'p')
                        {
                            objectToGenerate = room.furniture[15];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'q')
                        {
                            objectToGenerate = room.furniture[16];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'r')
                        {
                            objectToGenerate = room.furniture[17];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 's')
                        {
                            objectToGenerate = room.furniture[18];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 't')
                        {
                            objectToGenerate = room.furniture[19];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'u')
                        {
                            objectToGenerate = room.furniture[20];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'v')
                        {
                            objectToGenerate = room.furniture[21];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'w')
                        {
                            objectToGenerate = room.furniture[22];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'x')
                        {
                            objectToGenerate = room.furniture[23];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'y')
                        {
                            objectToGenerate = room.furniture[24];
                            x++;
                            nextC = x;
                        }
                        else if (layouts[x] == 'z')
                        {
                            objectToGenerate = room.furniture[25];
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
                            // tässä on huone.
                            GameObject _object = MasterManager.NetworkInstantiate(objectToGenerate, vec2, transform.rotation = new Quaternion(0, 0, 0, 0));
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
        }
    }
            
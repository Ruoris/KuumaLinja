
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Jobs;

public class RandomLevelGenerator : MonoBehaviour
{
    public GameObject floorGenerator;
    public GameObject levelController;

    public Room[] roomList;

    public int floorX;
    public int floorY;

    public int startDifficulty;

    public bool floorGenerated;

    public int numberOfRooms;
    public int i = 0;
    public int iTo;

    string yString;
    string xString;

    private int floorsGenerated = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
    }

    // Update is called once per frame
    void Update()
    {
       
            if (floorGenerated == false)
            {
                i++;
                floorsGenerated++;
                GenerateFloor();
                if (i == iTo)
                {
                    floorGenerated = true;
                }
            }
        
    }

    public void GenerateFloor()
    {
        
            GameObject floor = MasterManager.NetworkInstantiate(floorGenerator, transform.position = new Vector2(0, 0), transform.rotation = new Quaternion(0, 0, 0, 0));
            GameObject[] temp = new GameObject[iTo];
            levelController.GetComponent<LevelController>().floors.CopyTo(temp, 0);
            levelController.GetComponent<LevelController>().floors = temp;
            levelController.GetComponent<LevelController>().floors[i - 1] = floor;
            levelController.GetComponent<LevelController>().numberOfFloors++;

            //Make the Size Of Floor
            floor.GetComponent<LevelGenerator>().floorX = floorX;
            floor.GetComponent<LevelGenerator>().floorY = floorY;

            //Make the exit and Entrance of the floor
            int randomX;
            int randomY;

            int floorSpawnX = 0;
            int floorSpawnY = 0;

            string _floorPoint = "";

            int roomsStartX = 0;
            int roomsStartY = 0;
            int roomsEndX = 0;
            int roomsEndY = 0;

            int roomLocationX = 0;
            int roomLocationY = 0;

            int previousDoorDir = -1;

            if (levelController.GetComponent<LevelController>().floorSpawnX == 0 && levelController.GetComponent<LevelController>().floorSpawnY == 0)
            {
                randomX = 3;
                randomY = 2;

                floorSpawnX = randomX;
                floorSpawnY = randomY;

                _floorPoint = floorSpawnX + "-" + floorSpawnY;
            }
            else
            {
                floorSpawnX = levelController.GetComponent<LevelController>().floorSpawnX;
                floorSpawnY = levelController.GetComponent<LevelController>().floorSpawnY;
                _floorPoint = floorSpawnX + "-" + floorSpawnY;
            }
            floor.GetComponent<LevelGenerator>().floorSpawn = _floorPoint;

            if (levelController.GetComponent<LevelController>().currentFloor == 0)
            {
                floorGenerator.GetComponent<LevelGenerator>().playerSpawn = new Vector2(floorSpawnX * 0.32f, floorSpawnY * 0.32f);
            }

            if (floorSpawnY > floorY / 2)
            {
                if (floorSpawnX > floorX / 2)
                {
                    randomX = 3;
                }
                else
                {
                    randomX = floorX - 2;
                }
                randomY = 2;
            }
            else
            {
                if (floorSpawnX > floorX / 2)
                {
                    randomX = 3;
                }
                else
                {
                    randomX = floorX - 2;
                }
                randomY = floorY - 3;
            }

            int floorExitX = randomX;
            int floorExitY = randomY;

            _floorPoint = floorExitX + "-" + floorExitY;

            levelController.GetComponent<LevelController>().floorSpawnX = floorExitX;
            levelController.GetComponent<LevelController>().floorSpawnY = floorExitY;

            floor.GetComponent<LevelGenerator>().nextFloor = _floorPoint;

            int totalX = 0;
            int totalY = 0;

            //How many Rooms
            for (int x = 0; x < numberOfRooms; x++)
            {
                Room[] temp2 = new Room[numberOfRooms];
                int totalDif = 0;

                floor.GetComponent<LevelGenerator>().rooms.CopyTo(temp2, 0);
                floor.GetComponent<LevelGenerator>().rooms = temp2;
                int randomRoom = UnityEngine.Random.Range(1, roomList.Length - 1);

                //Elevators
                if (x == 0 || x == 1)
                {
                    Room _room = floor.GetComponent<LevelGenerator>().rooms[x] = roomList[0];
                }
                else
                {
                    Room _room = floor.GetComponent<LevelGenerator>().rooms[x] = roomList[randomRoom];
                    totalDif += _room.difficulty;
                }
                //Room Locations
                string[] temp3 = new string[numberOfRooms];
                floor.GetComponent<LevelGenerator>().roomCoordinates.CopyTo(temp3, 0);
                floor.GetComponent<LevelGenerator>().roomCoordinates = temp3;

                if (x == 0)
                {
                    floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (floorSpawnX - 3) + "-" + (floorSpawnY - 2);
                    if (floorSpawnX < floorX / 2)
                    {
                        roomsStartX = floorSpawnX + 2;

                        if (floorSpawnY < floorY / 2)
                        {
                            roomsStartY = floorSpawnY + 3;
                        }
                        else
                        {
                            roomsStartY = floorSpawnY - 2;
                        }
                    }
                    else
                    {
                        roomsStartX = floorSpawnX - 3;

                        if (floorSpawnY < floorY / 2)
                        {
                            roomsStartY = floorSpawnY + 3;
                        }
                        else
                        {
                            roomsStartY = floorSpawnY - 2;
                        }
                    }
                }
                else if (x == 1)
                {
                    floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (floorExitX - 3) + "-" + (floorExitY - 2);
                    if (floorSpawnX < floorX / 2)
                    {
                        roomsEndX = floorSpawnX + 2;

                        if (floorSpawnY < floorY / 2)
                        {
                            roomsEndY = floorSpawnY + 3;
                        }
                        else
                        {
                            roomsEndY = floorSpawnY - 2;
                        }
                    }
                    else
                    {
                        roomsEndX = floorSpawnX - 3;

                        if (floorSpawnY < floorY / 2)
                        {
                            roomsEndY = floorSpawnY + 3;
                        }
                        else
                        {
                            roomsEndY = floorSpawnY - 2;
                        }
                    }
                }
                else
                {
                    randomX = roomsStartX;
                    randomY = roomsStartY;

                    roomLocationX = randomX;
                    roomLocationY = randomY;

                    totalX += randomX;
                    totalY += randomY;
                    floor.GetComponent<LevelGenerator>().roomCoordinates[x] = 0 + "-" + 0;
                }

                //Room Sizes
                string[] temp4 = new string[numberOfRooms];
                floor.GetComponent<LevelGenerator>().roomSizes.CopyTo(temp4, 0);
                floor.GetComponent<LevelGenerator>().roomSizes = temp4;

                if (x == 0 || x == 1)
                {
                    floor.GetComponent<LevelGenerator>().roomSizes[x] = "5-5";
                }
                else
                {
                    floor.GetComponent<LevelGenerator>().roomSizes[x] = floorX + "-" + floorY;

                    if (floorSpawnX > floorX / 2)
                    {
                        if (floorSpawnY > floorY / 2)
                        {
                            floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (roomLocationX - randomX) + "-" + (roomLocationY - randomY);
                        }
                        else
                        {
                            floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (roomLocationX - randomX) + "-" + (roomLocationY);
                        }
                    }
                    else
                    {
                        if (floorSpawnY > floorY / 2)
                        {
                            floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (roomLocationX) + "-" + (roomLocationY - randomY);
                        }
                    }
                }

                int roomSizeY = randomY;
                int roomSizeX = randomX;

                //Room Doors
                string[] temp5 = new string[numberOfRooms];
                floor.GetComponent<LevelGenerator>().roomDoors.CopyTo(temp5, 0);
                floor.GetComponent<LevelGenerator>().roomDoors = temp5;

                if (x == 0)
                {
                    if (floorSpawnX < floorX / 2)
                    {
                        floor.GetComponent<LevelGenerator>().roomDoors[x] = "4-2";
                    }
                    else
                    {
                        floor.GetComponent<LevelGenerator>().roomDoors[x] = "0-2";
                    }
                }
                else if (x == 1)
                {
                    if (floorExitX < floorX / 2)
                    {
                        floor.GetComponent<LevelGenerator>().roomDoors[x] = "4-2";
                    }
                    else
                    {
                        floor.GetComponent<LevelGenerator>().roomDoors[x] = "0-2";
                    }
                }
                else
                {
                    if (previousDoorDir == -1)
                    {
                        previousDoorDir = UnityEngine.Random.Range(0, 1);
                    }
                    else if (previousDoorDir == 0)
                    {
                        previousDoorDir = 1;
                    }
                    else if (previousDoorDir == 1)
                    {
                        previousDoorDir = 0;
                    }
                    randomX = UnityEngine.Random.Range(1, roomSizeX - 2);
                    xString = randomX + "-" + 0;
                    if (previousDoorDir == 0)
                    {
                        randomY = UnityEngine.Random.Range(1, roomSizeY - 1);
                        yString = 0 + "-" + randomY;
                    }
                    else
                    {
                        Debug.Log("RightWall!");
                        randomY = UnityEngine.Random.Range(1, roomSizeY - 1);
                        yString = (roomSizeX - 2) + "-" + randomY;
                    }
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = xString + ", " + yString;
                }

                //Room Furniture
                string[] temp6 = new string[numberOfRooms];
                floor.GetComponent<LevelGenerator>().roomLayouts.CopyTo(temp6, 0);
                floor.GetComponent<LevelGenerator>().roomLayouts = temp6;
                int totalFurX = 1;
                int totalFurY = 0;
                string objectMaking = "";
                int laskinX = floorX / 10;
                int laskinY = floorY / 10;

                if (x > 1)
                {
                    for (int i = 0; i < laskinY; i++)
                    {
                        for (int j = 0; j < laskinX; j++)
                        {
                            int randomFurniture = UnityEngine.Random.Range(2, floor.GetComponent<LevelGenerator>().rooms[x].furniture.Length);
                            int roomFurnitureX = UnityEngine.Random.Range(totalFurX, totalFurX);
                            int roomFurnitureY = UnityEngine.Random.Range(totalFurY, totalFurY);
                            if ((j == 0 && i == 0) || (i == laskinY - 1 && j == laskinX - 1))
                            {
                                objectMaking = "a" + roomFurnitureX + "-" + roomFurnitureY + ",";
                                if (floorsGenerated % 3 == 0)
                                {
                                    objectMaking = "b" + roomFurnitureX + "-" + roomFurnitureY + ",";
                                }
                            }
                            else if (randomFurniture == 2)
                            {
                                objectMaking = "c" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 3)
                            {
                                objectMaking = "d" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 4)
                            {
                                objectMaking = "e" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 5)
                            {
                                objectMaking = "f" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 6)
                            {
                                objectMaking = "g" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 7)
                            {
                                objectMaking = "h" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 8)
                            {
                                objectMaking = "i" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 9)
                            {
                                objectMaking = "j" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 10)
                            {
                                objectMaking = "k" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 11)
                            {
                                objectMaking = "l" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 12)
                            {
                                objectMaking = "m" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 13)
                            {
                                objectMaking = "n" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 14)
                            {
                                objectMaking = "o" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 15)
                            {
                                objectMaking = "p" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 16)
                            {
                                objectMaking = "q" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 17)
                            {
                                objectMaking = "r" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 18)
                            {
                                objectMaking = "s" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 19)
                            {
                                objectMaking = "t" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 20)
                            {
                                objectMaking = "u" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 21)
                            {
                                objectMaking = "v" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 22)
                            {
                                objectMaking = "w" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 23)
                            {
                                objectMaking = "x" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 24)
                            {
                                objectMaking = "y" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }
                            else if (randomFurniture == 25)
                            {
                                objectMaking = "z" + roomFurnitureX + "-" + roomFurnitureY + ",";
                            }

                            floor.GetComponent<LevelGenerator>().roomLayouts[x] += objectMaking;
                            totalFurX += 10;
                        }
                        totalFurX = 1;
                        totalFurY += 10;
                    }
                }
            }
        
    }
}

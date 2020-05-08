using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

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
    int i = 0;

    string yString;
    string xString;

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
            GenerateFloor();
            if (i == 2)
            {
                floorGenerated = true;
            }
        }
    }

    public void GenerateFloor()
    {
        GameObject floor = Instantiate(floorGenerator, transform.position = new Vector2(0, 0), transform.rotation = new Quaternion(0, 0, 0, 0));
        GameObject[] temp = new GameObject[numberOfRooms];
        levelController.GetComponent<LevelController>().floors.CopyTo(temp, 0);
        levelController.GetComponent<LevelController>().floors = temp;
        levelController.GetComponent<LevelController>().floors[i - 1] = floor;
        levelController.GetComponent<LevelController>().numberOfFloors++;

        //Make the Size Of Floor
        floor.GetComponent<LevelGenerator>().floorX = floorX;
        floor.GetComponent<LevelGenerator>().floorY = floorY;

        //Make the exit and Entrance of the floor
        int randomX = UnityEngine.Random.Range(1, floorX - 1);
        int randomY = UnityEngine.Random.Range(1, floorY - 1);

        int floorSpawnX = randomX;
        int floorSpawnY = randomY;

        string _floorPoint = floorSpawnX + "-" + floorSpawnY;

        floor.GetComponent<LevelGenerator>().floorSpawn = _floorPoint;


        if (floorSpawnY > floorY / 2)
        {
            randomX = UnityEngine.Random.Range(1, floorX - 1);
            randomY = UnityEngine.Random.Range(1, floorY - 1);
        }
        else
        {
            randomX = UnityEngine.Random.Range(1, floorX - 1);
            randomY = UnityEngine.Random.Range(floorY / 2, floorY - 1);
        }

        int floorExitX = randomX;
        int floorExitY = randomY;

        int roomLocationX;
        int roomLocationY;

        int totalX = 0;
        int totalY = 0;



        _floorPoint = floorExitX + "-" + floorExitY;
        floor.GetComponent<LevelGenerator>().nextFloor = _floorPoint;

        for (int x = 0; x < numberOfRooms; x++)
        {
            //How many Rooms
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
                floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (floorSpawnX - 2) + "-" + (floorSpawnY - 1);
            }
            else if (x == 1)
            {
                floor.GetComponent<LevelGenerator>().roomCoordinates[x] = (floorExitX - 2) + "-" + (floorExitY - 1);
            }
            else
            {
                randomX = UnityEngine.Random.Range(totalX, totalX + 5);
                randomY = UnityEngine.Random.Range(totalY, totalY + 5);
                roomLocationX = randomX;
                roomLocationY = randomY;

                totalX += randomX - totalX;
                totalY += randomY - totalY;
                floor.GetComponent<LevelGenerator>().roomCoordinates[x] = roomLocationX + "-" + roomLocationY;
            }

            //Room Sizes
            string[] temp4 = new string[numberOfRooms];
            floor.GetComponent<LevelGenerator>().roomSizes.CopyTo(temp4, 0);
            floor.GetComponent<LevelGenerator>().roomSizes = temp4;

            if (x == 0 || x == 1)
            {
                floor.GetComponent<LevelGenerator>().roomSizes[x] = "3-3";
            }
            else
            {
                randomX = UnityEngine.Random.Range(8, 20);
                randomY = UnityEngine.Random.Range(8, 20);
                totalX += randomX;
                totalY += randomY;

                if (totalX > floorX)
                {
                    randomX -= totalX - floorX;
                    Debug.Log("X Over");
                }
                else if (totalX + 3 > floorX)
                {
                    randomX += totalX + 3 - floorX;
                    Debug.Log("X: " + randomX);
                }

                if (totalY > floorY)
                {
                    randomY -= totalY - floorY;
                    Debug.Log("Y Over");
                }
                else if (totalY + 3 > floorY)
                {
                    randomY += totalY + 3 - floorY;
                    Debug.Log("X: " + randomX);
                }

                if (randomX < 3)
                {

                }

                floor.GetComponent<LevelGenerator>().roomSizes[x] = randomX + "-" + randomY;
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
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = "2-1";
                }
                else
                {
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = "0-1";
                }
            }
            else if (x == 1)
            {
                if (floorExitX < floorX / 2)
                {
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = "2-1";
                }
                else
                {
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = "0-1";
                }
            }
            else
            {
                int randomNumber = UnityEngine.Random.Range(0, 0);
                if (randomNumber == 0)
                {
                    randomNumber = UnityEngine.Random.Range(0, 1);

                    if (randomNumber == 0)
                    {
                        if (randomX < floorX / 2)
                        {
                            randomY = UnityEngine.Random.Range(1, roomSizeY - 2);
                            floor.GetComponent<LevelGenerator>().roomDoors[x] = (roomSizeX - 1) + "-" + randomY;
                        }
                        else
                        {
                            randomY = UnityEngine.Random.Range(1, roomSizeY - 2);
                            floor.GetComponent<LevelGenerator>().roomDoors[x] = 0 + "-" + randomY;
                        }
                    }
                    else if (randomNumber == 1)
                    {
                        if (randomY < floorY / 2)
                        {
                            randomX = UnityEngine.Random.Range(1, roomSizeX - 2);
                            floor.GetComponent<LevelGenerator>().roomDoors[x] = randomX + "-" + (roomSizeY - 1);
                        }
                        else
                        {
                            randomX = UnityEngine.Random.Range(1, roomSizeX - 2);
                            floor.GetComponent<LevelGenerator>().roomDoors[x] = randomX + "-" + 0;
                        }
                    }
                }
                else
                {

                    if (randomY < floorY / 2)
                    {
                        randomX = UnityEngine.Random.Range(1, roomSizeX - 2);
                        yString = randomX + "-" + (roomSizeY - 1);
                    }
                    else
                    {
                        randomX = UnityEngine.Random.Range(1, roomSizeX - 2);
                        yString = randomX + "-" + 0;
                    }

                    if (randomX < floorX / 2)
                    {
                        randomY = UnityEngine.Random.Range(1, roomSizeY - 2);
                        xString = randomY + "-" + (roomSizeY - 1);
                    }
                    else
                    {
                        randomX = UnityEngine.Random.Range(1, roomSizeY - 2);
                        yString = randomY + "-" + 0;
                    }
                    floor.GetComponent<LevelGenerator>().roomDoors[x] = xString + ", " + yString;

                }
            }

            //Room Furniture
            string[] temp6 = new string[numberOfRooms];
            floor.GetComponent<LevelGenerator>().roomLayouts.CopyTo(temp6, 0);
            floor.GetComponent<LevelGenerator>().roomLayouts = temp6;

        }
    }
}

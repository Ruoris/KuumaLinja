using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Jobs;

public class RandomLevelGeneratorMultiplayer : MonoBehaviour
{
    public GameObject floorGenerator;
    public GameObject levelController;
    //Listat
    public Room[] roomList;

    public int[] randomrooms;
    public int[] randomXarray;
    public int[] randomYarray;
    public int[] randomFurniturearray = new int[23];

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
    
    public bool randomized = false;

    public void Randomizing()
    {   //Huoneet
        int randomroom0 = UnityEngine.Random.Range(1, 8 - 1);
        int randomroom1 = UnityEngine.Random.Range(1, 8 - 1);
        int randomroom2 = UnityEngine.Random.Range(1, 8 - 1);
        //Ovet
        
        // randomikoordinaatit.
        //X koordinaatit
        int randomX0 = UnityEngine.Random.Range(1, 5 - 2);
        int randomX1 = UnityEngine.Random.Range(1, 25 - 2);
        int randomX2 = UnityEngine.Random.Range(1, 25 - 2);
        // Y koordinaatit
        int randomY0 = UnityEngine.Random.Range(1, 5 - 1);
        int randomY1 = UnityEngine.Random.Range(1, 25 - 1);
        int randomY2 = UnityEngine.Random.Range(1, 25 - 1);
        //roomfurnitureX

        int[] randomFurniture = new int[18];
        for(int x = 0; x < 18; x++)
        {
        randomFurniture[x]= UnityEngine.Random.Range(2, 23);
        }
        // huoneiden ja randomikoordinaattien array siirtoa varten
        int[] randoms= new int[9];
        randoms[0] = randomroom0;
        randoms[1] = randomroom1;
        randoms[2] = randomroom2;
        randoms[3] = randomX0;
        randoms[4] = randomX1;
        randoms[5] = randomX2;
        randoms[6] = randomY0;
        randoms[7] = randomY1;
        randoms[8] = randomY2;

        PhotonView photonview = PhotonView.Get(this);
        photonview.RPC("GetRandoms", RpcTarget.AllViaServer, randoms, randomFurniture);
        Debug.Log("Randomisoitu");
    }

    [PunRPC]
    void GetRandoms(int[]randoms,int[]randomfurniture)
    {
        randomrooms = new int[3];


        for (int x = 0; x < 3;x++)
        {
        randomrooms[x] = randoms[x];
        }
        //Alustus
        randomXarray = new int[3];
        randomYarray = new int[3];

        randomXarray[0] = randoms[3];
        randomXarray[1] = randoms[4];
        randomXarray[2] = randoms[5];

        randomYarray[0] = randoms[6];
        randomYarray[1] = randoms[7];
        randomYarray[2] = randoms[8];
        randomfurniture.CopyTo(randomFurniturearray,0);
        randomized = true;
        Debug.Log("randomisaatio tuli perille");
    }
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");

        if (PhotonNetwork.IsMasterClient)
        {
        Randomizing();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (floorGenerated == false && randomized == true )
            {
                i++;
                floorsGenerated++;
                GenerateFloor();
                if (i == iTo)
                {
                    floorGenerated = true;
                    randomized = false; 
                }
                if (PhotonNetwork.IsMasterClient)
                {
                    Randomizing();
                }
               
            }
        
    }

    public void GenerateFloor()
    {
        
            GameObject floor = Instantiate(floorGenerator, transform.position = new Vector2(0, 0), transform.rotation = new Quaternion(0, 0, 0, 0));
            GameObject[] temp = new GameObject[iTo];
            levelController.GetComponent<LevelControllerMultiplayer>().floors.CopyTo(temp, 0);
            levelController.GetComponent<LevelControllerMultiplayer>().floors = temp;
            levelController.GetComponent<LevelControllerMultiplayer>().floors[i - 1] = floor;
            levelController.GetComponent<LevelControllerMultiplayer>().numberOfFloors++;
            
            //Make the Size Of Floor
            floor.GetComponent<LevelGeneratorMultiplayer>().floorX = floorX;
            floor.GetComponent<LevelGeneratorMultiplayer>().floorY = floorY;

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

            if (levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnX == 0 && levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnY == 0)
            {
                randomX = 3;
                randomY = 2;

                floorSpawnX = randomX; // 3
                floorSpawnY = randomY; // 2

                _floorPoint = floorSpawnX + "-" + floorSpawnY;
            }
            else
            {
                floorSpawnX = levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnX; // 3
                floorSpawnY = levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnY; // 2
                _floorPoint = floorSpawnX + "-" + floorSpawnY;
            }
            floor.GetComponent<LevelGeneratorMultiplayer>().floorSpawn = _floorPoint;

            if (levelController.GetComponent<LevelControllerMultiplayer>().currentFloor == 0)
            {
                floorGenerator.GetComponent<LevelGeneratorMultiplayer>().playerSpawn = new Vector2(floorSpawnX * 0.32f, floorSpawnY * 0.32f);
            }

            if (floorSpawnY > floorY / 2)   // 2 > 30/2 
            {
                if (floorSpawnX > floorX / 2)   //3 > 30/2
                {
                    randomX = 3;            // randomx on 3 ja floorspawn 3
                }
                else
                {
                    randomX = floorX - 2;
                }
                randomY = 2;
            }
            else              // tämän pitäisi olla aina true
            {
                if (floorSpawnX > floorX / 2) //  3 > 15 ei tosi
                {
                    randomX = 3;
                }
                else
                {
                    randomX = floorX - 2;  // 3= 3-2
                }
                randomY = floorY - 3;   //  2-3 =-1 
            }

            int floorExitX = randomX; // Randomx on 1
            int floorExitY = randomY; // RandomY on -1

            _floorPoint = floorExitX + "-" + floorExitY;

            levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnX = floorExitX;
            levelController.GetComponent<LevelControllerMultiplayer>().floorSpawnY = floorExitY;

            floor.GetComponent<LevelGeneratorMultiplayer>().nextFloor = _floorPoint;

            int totalX = 0;
            int totalY = 0;
            int c = 0;
            //How many Rooms
            for (int x = 0; x < numberOfRooms; x++)
            {
                Room[] temp2 = new Room[numberOfRooms];
                int totalDif = 0;

                floor.GetComponent<LevelGeneratorMultiplayer>().rooms.CopyTo(temp2, 0);
                floor.GetComponent<LevelGeneratorMultiplayer>().rooms = temp2;
                int randomRoom = randomrooms[x];             //UnityEngine.Random.Range(1, roomList.Length - 1);

                //Elevators
                if (x == 0 || x == 1)
                {
                    Room _room = floor.GetComponent<LevelGeneratorMultiplayer>().rooms[x] = roomList[0];
                }
                else
                {
                    Room _room = floor.GetComponent<LevelGeneratorMultiplayer>().rooms[x] = roomList[randomRoom];
                    totalDif += _room.difficulty;
                }
                //Room Locations
                string[] temp3 = new string[numberOfRooms];
                floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates.CopyTo(temp3, 0);
                floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates = temp3;

                if (x == 0)
                {
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = (floorSpawnX - 3) + "-" + (floorSpawnY - 2);
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
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = (floorExitX - 3) + "-" + (floorExitY - 2);
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
                    Debug.Log("roomstartx "+roomsStartX);
                    Debug.Log("roomstarty " + roomsStartY);
                    randomX = roomsStartX;
                    randomY = roomsStartY;

                    roomLocationX = randomX;
                    roomLocationY = randomY;

                    totalX += randomX;
                    totalY += randomY;
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = 0 + "-" + 0;
                }

                //Room Sizes
                string[] temp4 = new string[numberOfRooms];               
                floor.GetComponent<LevelGeneratorMultiplayer>().roomSizes.CopyTo(temp4, 0);
                floor.GetComponent<LevelGeneratorMultiplayer>().roomSizes = temp4;

                if (x == 0 || x == 1)
                {
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomSizes[x] = "5-5";
                }
                else
                {
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomSizes[x] = floorX + "-" + floorY;

                    if (floorSpawnX > floorX / 2)
                    {
                        if (floorSpawnY > floorY / 2)
                        {
                            floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = (roomLocationX - randomX) + "-" + (roomLocationY - randomY);
                        }
                        else
                        {
                            floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = (roomLocationX - randomX) + "-" + (roomLocationY);
                        }
                    }
                    else
                    {
                        if (floorSpawnY > floorY / 2)
                        {
                            floor.GetComponent<LevelGeneratorMultiplayer>().roomCoordinates[x] = (roomLocationX) + "-" + (roomLocationY - randomY);
                        }
                    }
                }

                int roomSizeY = randomY;
                int roomSizeX = randomX;

                //Room Doors
                string[] temp5 = new string[numberOfRooms];
                floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors.CopyTo(temp5, 0);
                floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors = temp5;

                if (x == 0)
                {
                    if (floorSpawnX < floorX / 2)
                    {
                        floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors[x] = "4-2";
                    }
                    else
                    {
                        floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors[x] = "0-2";
                    }
                }
                else if (x == 1)
                {
                    if (floorExitX < floorX / 2)
                    {
                        floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors[x] = "4-2";
                    }
                    else
                    {
                        floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors[x] = "0-2";
                    }
                }
                else
                {
                    if (previousDoorDir == -1)
                    {
                        Debug.Log("previousDoorDir");
                        previousDoorDir =0;
                    }
                    else if (previousDoorDir == 0)
                    {
                        previousDoorDir = 1;
                    }
                    else if (previousDoorDir == 1)
                    {
                        previousDoorDir = 0;
                    }Debug.Log("RandomX: " +roomSizeX);
                randomX = randomXarray[x];    //UnityEngine.Random.Range(1, roomSizeX - 2);
                    xString = randomX + "-" + 0;
                    if (previousDoorDir == 0)
                    {
                        Debug.Log("RandomX");
                    randomY = randomYarray[x];  //UnityEngine.Random.Range(1, roomSizeY - 1);
                        yString = 0 + "-" + randomY;
                    }
                    else
                    {
                        Debug.Log("RandomX");
                        Debug.Log("RightWall!");
                    randomY = randomYarray[x];      // UnityEngine.Random.Range(1, roomSizeY - 1);
                        yString = (roomSizeX - 2) + "-" + randomY;
                    }
                    floor.GetComponent<LevelGeneratorMultiplayer>().roomDoors[x] = xString + ", " + yString;
                }

                //Room Furniture
                string[] temp6 = new string[numberOfRooms];
                floor.GetComponent<LevelGeneratorMultiplayer>().roomLayouts.CopyTo(temp6, 0);
                floor.GetComponent<LevelGeneratorMultiplayer>().roomLayouts = temp6;
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
                            int randomFurniture = randomFurniturearray[c];     //UnityEngine.Random.Range(2, floor.GetComponent<LevelGeneratorMultiplayer>().rooms[x].furniture.Length);
                            c++;
                            int roomFurnitureX =  totalFurX;
                            int roomFurnitureY =  totalFurY;
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

                            floor.GetComponent<LevelGeneratorMultiplayer>().roomLayouts[x] += objectMaking;
                            totalFurX += 10;
                        }
                        totalFurX = 1;
                        totalFurY += 10;
                    }

                }
               
            }
        
    }
}

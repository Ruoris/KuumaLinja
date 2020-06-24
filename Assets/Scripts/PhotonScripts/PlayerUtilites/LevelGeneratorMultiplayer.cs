using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class LevelGeneratorMultiplayer : MonoBehaviour
{
    public Room[] rooms;
    public string[] roomCoordinates;
    public string[] roomSizes;
    public string[] roomDoors;
    public string[] roomLayouts;

    public int floorX, floorY;

    public string floorSpawn;
    public string nextFloor;

    public GameObject player;
    public GameObject aim;

    public GameObject hallwayFloor;
    public GameObject hallwayCorners;
    public GameObject wallFloor;
    public GameObject stairs;

    private Vector2 location;
    public Vector2 playerSpawn;

    public GameObject levelController;
    public GameObject levelGenerator;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            location = new Vector2(0, 0);

            levelController = GameObject.FindGameObjectWithTag("LevelController");
            levelGenerator = this.gameObject;

            //mainCamera.transform.position = new Vector2((floorX / 2) * 0.32f, (floorY / 2) * 0.32f);
            //mainCamera.GetComponent<Camera>().orthographicSize = floorX;

            GenerateFloor();
            GeneratePoints();

            if (levelController.GetComponent<LevelController>().playerSpawned == false)
            {
                GameObject _player = /* Instantiate*/  MasterManager.NetworkInstantiate(player, playerSpawn, transform.rotation = new Quaternion(0, 0, 0, 0));

                GameObject _aim = MasterManager.NetworkInstantiate(aim, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                //_player.GetComponent<PlayerCtrl>().aim = _aim;
                levelController.GetComponent<LevelController>().playerSpawned = true;
            }
            GetRooms();
            this.gameObject.SetActive(false);
        }
    }



    public void GenerateFloor()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            float _x = 0;
            float _y = 0;

            while (floorY > _y)
            {
                while (floorX > _x)
                {
                    location.x = location.x + 0.32f;
                    GameObject floor = MasterManager.NetworkInstantiate(hallwayFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                    floor.transform.parent = levelController.transform;

                    //CornerGenerator
                    if (_x == 0 && _y == 0)
                    {
                        GameObject downLeftCorner = MasterManager.NetworkInstantiate(hallwayCorners, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                        downLeftCorner.transform.parent = levelController.transform;
                    }

                    if (_x == floorX - 1 && _y == 0)
                    {
                        GameObject downRightCorner = MasterManager.NetworkInstantiate(hallwayCorners, location, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        downRightCorner.transform.parent = levelController.transform;
                    }

                    if (_x == 0 && _y == floorY - 1)
                    {
                        GameObject topLeftCorner = MasterManager.NetworkInstantiate(hallwayCorners, location, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        topLeftCorner.transform.parent = levelController.transform;
                    }

                    if (_x == floorX - 1 && _y == floorY - 1)
                    {
                        GameObject topRightCorner = MasterManager.NetworkInstantiate(hallwayCorners, location, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        topRightCorner.transform.parent = levelController.transform;
                    }

                    _x++;
                    if (_y == 0)
                    {
                        GameObject wall = MasterManager.NetworkInstantiate(wallFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                        wall.transform.parent = levelController.transform;
                    }
                    if (_y == floorY - 1)
                    {
                        GameObject wall = MasterManager.NetworkInstantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                        wall.transform.parent = levelController.transform;
                    }
                    if (_x == 1)
                    {
                        GameObject wall = MasterManager.NetworkInstantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                        wall.transform.parent = levelController.transform;
                    }
                    if (_x == floorX)
                    {
                        GameObject wall = MasterManager.NetworkInstantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
                        wall.transform.parent = levelController.transform;
                    }
                }

                if (_x == floorX && _y < floorY)
                {
                    _x = 0;
                    location.x = 0;
                    location.y = location.y + 0.32f;
                    _y++;
                }
            }
        }
    }

    public void GeneratePoints()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Spawn point
            int spawnXint = 0;
            int spawnYint = 0;
            for (int x = 0; x < floorSpawn.Length; x++)
            {
                if (floorSpawn[x] == '-')
                {
                    int save = x;
                    x = 0;
                    string spawnString = null;
                    while (x < save)
                    {
                        spawnString += floorSpawn[x];
                        x++;
                    }
                    int.TryParse(spawnString, out spawnXint);
                    x++;
                    spawnString = null;
                    while (x < floorSpawn.Length)
                    {
                        spawnString += floorSpawn[x];
                        x++;
                    }
                    int.TryParse(spawnString, out spawnYint);
                }
            }
            location = new Vector2(spawnXint * 0.32f, spawnYint * 0.32f);
            GameObject _stairs = MasterManager.NetworkInstantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
            _stairs.transform.parent = this.gameObject.transform;
            levelController.GetComponent<LevelController>().floorSpawn = location;
            playerSpawn = location;

            if (nextFloor[0] == '.')
            {
            }
            else
            {
                //Next Floor Enterance
                for (int x = 0; x < nextFloor.Length - 1; x++)
                {
                    if (nextFloor[x] == '-')
                    {
                        int save = x;
                        x = 0;
                        string spawnString = null;
                        while (x < save)
                        {
                            spawnString += nextFloor[x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnXint);
                        x++;
                        spawnString = null;
                        while (x < nextFloor.Length)
                        {
                            spawnString += nextFloor[x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnYint);
                    }
                }
                location = new Vector2(spawnXint * 0.32f, spawnYint * 0.32f);
                GameObject exit = MasterManager.NetworkInstantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                exit.transform.parent = this.gameObject.transform;
                exit.GetComponent<PointController>().nextFloor = true;
            }
        }
    }

    public void GetRooms()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int r = 0; r < roomCoordinates.Length; r++)
            {
                int spawnXint = 0;
                int spawnYint = 0;
                for (int x = 0; x < roomCoordinates[r].Length; x++)
                {
                    if (roomCoordinates[r][x] == '-')
                    {
                        int save = x;
                        x = 0;
                        string spawnString = null;
                        while (x < save)
                        {
                            spawnString += roomCoordinates[r][x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnXint);
                        x++;
                        spawnString = null;
                        while (x < roomCoordinates[r].Length)
                        {
                            spawnString += roomCoordinates[r][x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnYint);
                    }
                }
                location = new Vector2(spawnXint * 0.32f, spawnYint * 0.32f);

                spawnXint = 0;
                spawnYint = 0;

                for (int x = 0; x < roomSizes[r].Length; x++)
                {
                    if (roomSizes[r][x] == '-')
                    {
                        int save = x;
                        x = 0;
                        string spawnString = null;
                        while (x < save)
                        {
                            spawnString += roomSizes[r][x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnXint);
                        x++;
                        spawnString = null;
                        while (x < roomSizes[r].Length)
                        {
                            spawnString += roomSizes[r][x];
                            x++;
                        }
                        int.TryParse(spawnString, out spawnYint);
                    }
                }
                GameObject room = GetComponent<RoomGenerator>().GenerateRoom(rooms[r], location, spawnXint, spawnYint, roomDoors[r], levelGenerator);
                room.transform.rotation = new Quaternion(0, 0, 0, 0);

                Vector2 fixY = room.transform.position;
                fixY.x = fixY.x * -1;
                room.transform.position = fixY;

                GetComponent<RoomGenerator>().GenerateFurniture(roomLayouts[r], location, rooms[r], levelGenerator);
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
}
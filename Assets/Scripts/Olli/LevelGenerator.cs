﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Room[] rooms;

    public string[] roomCoordinates;
    public string[] roomSizes;
    public string[] roomDoors;
    public string[] roomWindows;
    public string[] roomLayouts;

    public int floorX, floorY;

    public string floorSpawn;
    public string nextFloor;

    public GameObject player;
    public GameObject aim;

    public GameObject fogTile;

    public GameObject hallwayFloor;
    public GameObject wallFloor;
    public GameObject stairs;

    private Vector2 location;
    private Vector2 playerSpawn;

    public Camera mainCamera;

    public GameObject levelController;

    // Start is called before the first frame update
    void Start()
    {
        location = new Vector2(0, 0);

        levelController = GameObject.FindGameObjectWithTag("LevelController");

        //mainCamera.transform.position = new Vector2((floorX / 2) * 0.32f, (floorY / 2) * 0.32f);
        //mainCamera.GetComponent<Camera>().orthographicSize = floorX;

        GenerateFloor();
        GeneratePoints();

        if (levelController.GetComponent<LevelController>().playerSpawned == false)
        {
            Vector2 temp2 = new Vector2(1, 1);
            GameObject _player = Instantiate(player, temp2, transform.rotation = new Quaternion(0, 0, 0, 0));
            GameObject _aim = Instantiate(aim, location, transform.rotation = new Quaternion(0, 0, 0, 0));
            _player.GetComponent<PlayerCtrl>().aim = _aim;
            levelController.GetComponent<LevelController>().playerSpawned = true;
        }

        GetRooms();
        
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateFloor()
    {
        float _x = 0;
        float _y = 0;

        while (floorY > _y)
        {
            while (floorX > _x)
            {
                location.x = location.x + 0.32f;
                GameObject floor = Instantiate(hallwayFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                floor.transform.parent = levelController.transform;
                //Instantiate(fogTile, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                _x++;
                if (_y == 0)
                {
                    GameObject wall = Instantiate(wallFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                    wall.transform.parent = levelController.transform;
                }
                if (_y == floorY - 1)
                {
                    GameObject wall = Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 180));
                    wall.transform.parent = levelController.transform;
                }
                if (_x == 1)
                {
                    GameObject wall = Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                    wall.transform.parent = levelController.transform;
                }
                if (_x == floorX)
                {
                    GameObject wall = Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
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

    public void GeneratePoints()
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
        GameObject _stairs = Instantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
        _stairs.transform.parent = this.gameObject.transform;

        playerSpawn = location;

        //Next Floor Enterance
        for (int x = 0; x < floorSpawn.Length; x++)
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
        GameObject exit = Instantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
        exit.transform.parent = this.gameObject.transform;
        exit.GetComponent<PointController>().nextFloor = true;
    }

    public void GetRooms()
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
            Debug.Log("Spawn:" + spawnXint + ", " + spawnYint);
            GetComponent<RoomGenerator>().GenerateRoom(rooms[r], location, spawnXint, spawnYint, roomDoors[r], roomWindows[r], this.gameObject);
            GetComponent<RoomGenerator>().GenerateFurniture(roomLayouts, location, rooms[r], this.gameObject);
        }
    }
}
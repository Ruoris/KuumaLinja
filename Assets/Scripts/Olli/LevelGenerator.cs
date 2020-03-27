using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int[] roomsX;
    public int[] roomsY;

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

    public GameObject roomController;
    public string floorLayout;

    // Start is called before the first frame update
    void Start()
    {
        location = new Vector2(0, 0);

        //mainCamera.transform.position = new Vector2((floorX / 2) * 0.32f, (floorY / 2) * 0.32f);
        //mainCamera.GetComponent<Camera>().orthographicSize = floorX;

        GenerateFloor();
        GeneratePoints();

        GameObject _player = Instantiate(player, playerSpawn, transform.rotation = new Quaternion(0, 0, 0, 0));
        GameObject _aim = Instantiate(aim, location, transform.rotation = new Quaternion(0, 0, 0, 0));
        _player.GetComponent<PlayerCtrl>().aim = _aim;
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
                Instantiate(hallwayFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                //Instantiate(fogTile, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                _x++;
                if (_y == 0)
                {
                    Instantiate(wallFloor, location, transform.rotation = new Quaternion(0, 0, 0, 0));
                }
                if (_y == floorY - 1)
                {

                    Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 180));

                }
                if (_x == 1)
                {
                    Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 270));
                }
                if (_x == floorX)
                {
                    Instantiate(wallFloor, location, transform.rotation = Quaternion.Euler(Vector3.forward * 90));
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
                    Debug.Log("X string: " + spawnString);
                    x++;
                }
                int.TryParse(spawnString, out spawnXint);
                Debug.Log("X int: " + spawnXint);
                x++;
                spawnString = null;
                while (x < floorSpawn.Length)
                {
                    spawnString += floorSpawn[x];
                    Debug.Log("Y string: " + spawnString);
                    x++;
                }
                int.TryParse(spawnString, out spawnYint);
                Debug.Log("Y int: " + spawnYint);
            }
        }
        location = new Vector2(spawnXint * 0.32f, spawnYint * 0.32f);
        Instantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
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
                    Debug.Log("X string: " + spawnString);
                    x++;
                }
                int.TryParse(spawnString, out spawnXint);
                Debug.Log("X int: " + spawnXint);
                x++;
                spawnString = null;
                while (x < nextFloor.Length)
                {
                    spawnString += nextFloor[x];
                    Debug.Log("Y string: " + spawnString);
                    x++;
                }
                int.TryParse(spawnString, out spawnYint);
                Debug.Log("Y int: " + spawnYint);
            }
        }
        location = new Vector2(spawnXint * 0.32f, spawnYint * 0.32f);
        GameObject exit = Instantiate(stairs, location, transform.rotation = new Quaternion(0, 0, 0, 0));
        exit.GetComponent<PointController>().nextFloor = true;
    }
}
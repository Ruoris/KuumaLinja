using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerMultiplayer : MonoBehaviour
{
    public int numberOfFloors;
    public int floorNum;

    public GameObject[] floors = new GameObject[2];
    public GameObject player;
    public Camera mainCamera;

    public int currentFloor;

    public Vector2 floorSpawn;
    public int floorSpawnX;
    public int floorSpawnY;

    [HideInInspector]
    public bool playerSpawned;
    [HideInInspector]
    public bool firstRoomSpawned;

    private GameObject rooms;

    public GameObject rndLvlGntr;

    public int levelsCleared = 1;

    public int enemiesKilled = 0;
    public int previousTotalEnemies = 0;
    public int previousFloor = 0;
    public float scorePoints = 0;

    public bool addPoints = false;

    public GameObject currentFloorObject;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        currentFloor = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Camera _mainCamera = Instantiate(mainCamera, player.transform.position + new Vector3(0, 0, -10), player.transform.rotation);
        }
        else
        {
            Camera _mainCamera = Instantiate(mainCamera, new Vector3(0, 0, -10), new Quaternion(0, 0, 0, 0));
        }

    }


    // Update is called once per frame
    void Update()
    {
        
            timer += Time.deltaTime;
            float coolDown = 0.25f;
            if (floors[0] != null && floors[0].activeSelf == false && firstRoomSpawned == false)
            {
                firstRoomSpawned = true;
                floors[currentFloor].SetActive(true);
            }

            if (currentFloor > floors.Length - 1 && currentFloor != 0)
            {
                currentFloor = 0;
                rndLvlGntr.GetComponent<RandomLevelGeneratorMultiplayer>().i = 0;
                rndLvlGntr.GetComponent<RandomLevelGeneratorMultiplayer>().floorGenerated = false;
                firstRoomSpawned = false;
            }

            if (previousFloor != currentFloor)
            {
                previousFloor = currentFloor;
                addPoints = true;
                if (currentFloorObject != null)
                {
                    timer = 0;
                }
            }
            if (timer > coolDown && currentFloorObject != null)
            {
                Debug.Log("SetFalse");
                currentFloorObject.SetActive(false);
            }
        
    }

    public void ResetFloor()
    {
        
            if (floorNum < numberOfFloors)
            {
                floorNum++;
            }
        

    }
}

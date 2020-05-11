using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int numberOfFloors;
    public int floorNum;

    public GameObject[] floors;

    public int currentFloor;

    public bool playerSpawned;

    public bool firstRoomSpawned;
    private GameObject rooms;

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = GameStatus.status.currentFloor;
        Debug.Log("LevelController");
            
    }

    // Update is called once per frame
    void Update()
    {
        if (floors[currentFloor].activeSelf == false && firstRoomSpawned == false)
        {
            firstRoomSpawned = true;
            floors[currentFloor].SetActive(true);
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

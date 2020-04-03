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

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = 0;
        floors[currentFloor].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetFloor()
    {
        if (floorNum < numberOfFloors)
        {
            floorNum++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}

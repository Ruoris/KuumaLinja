using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public bool playerSpawn;
    public bool nextFloor;

    public int currentFloor;

    private bool floorChanged;

    public GameObject levelController;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        currentFloor = levelController.GetComponent<LevelController>().currentFloor;
    }

    // Update is called once per frame
    void Update()
    {
        if (floorChanged == false)
        {
            levelController.GetComponent<LevelController>().currentFloor = currentFloor;
            floorChanged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (nextFloor == true)
            {
                levelController.GetComponent<LevelController>().floors[currentFloor].SetActive(false);
                currentFloor++;
                levelController.GetComponent<LevelController>().currentFloor++;
                if (currentFloor <= levelController.GetComponent<LevelController>().floors.Length)
                {
                    levelController.GetComponent<LevelController>().floors[currentFloor].SetActive(true);
                }
            }
        }
    }
}

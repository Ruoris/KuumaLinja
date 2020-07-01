using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointController : MonoBehaviour
{
    public bool playerSpawn;
    public bool nextFloor;

    public int currentFloor;

    private bool floorChanged;

    public GameObject levelController;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "MultiplayerEndless")
        {
            levelController = GameObject.FindGameObjectWithTag("LevelController");
            currentFloor = levelController.GetComponent<LevelControllerMultiplayer>().currentFloor;
            if (nextFloor == false)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r - 1,
                    this.gameObject.GetComponent<SpriteRenderer>().color.b,
                    this.gameObject.GetComponent<SpriteRenderer>().color.g - 1,
                    this.gameObject.GetComponent<SpriteRenderer>().color.a);
            }

        }
        if (scene.name != "MultiplayerEndless")
        {
            levelController = GameObject.FindGameObjectWithTag("LevelController");
            currentFloor = levelController.GetComponent<LevelController>().currentFloor;
            if (nextFloor == false)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r - 1,
                    this.gameObject.GetComponent<SpriteRenderer>().color.b,
                    this.gameObject.GetComponent<SpriteRenderer>().color.g - 1,
                    this.gameObject.GetComponent<SpriteRenderer>().color.a);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (scene.name == "MultiplayerEndless")
        {
            if (floorChanged == false)
            {
                levelController.GetComponent<LevelControllerMultiplayer>().currentFloor = currentFloor;
                floorChanged = true;
            }
        }
            if (scene.name != "MultiplayerEndless")
        {
            if (floorChanged == false)
            {
                levelController.GetComponent<LevelController>().currentFloor = currentFloor;
                floorChanged = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (scene.name == "MultiplayerEndless")
        {

            if (collision.gameObject.tag == "Player")
            {
                if (nextFloor == true)
                {
                    if (levelController.GetComponent<LevelControllerMultiplayer>().currentFloor + 1 != levelController.GetComponent<LevelController>().floors.Length)
                    {
                        levelController.GetComponent<LevelControllerMultiplayer>().floors[currentFloor].SetActive(false);
                    }
                    else
                    {
                        levelController.GetComponent<LevelControllerMultiplayer>().currentFloorObject = levelController.GetComponent<LevelController>().floors[currentFloor];
                    }
                    currentFloor++;
                    levelController.GetComponent<LevelControllerMultiplayer>().currentFloor++;

                    if (levelController.GetComponent<LevelControllerMultiplayer>().currentFloor == levelController.GetComponent<LevelController>().floors.Length)
                    {
                    }
                    else if (currentFloor <= levelController.GetComponent<LevelControllerMultiplayer>().floors.Length)
                    {
                        levelController.GetComponent<LevelControllerMultiplayer>().floors[currentFloor].SetActive(true);
                    }
                    levelController.GetComponent<LevelControllerMultiplayer>().levelsCleared++;
                }

                //Ruoriksen koodi

                //if (collision.gameObject.CompareTag("Player")) { floor1.SetActive(false); floor2.SetActive(true); GameObject[] corpse = (GameObject[])FindObjectsOfType(typeof(GameObject)); for (int i = 0; i < corpse.Length; i++) { if (corpse[i].name.Contains("Death") || corpse[i].name.Contains("Drop")) { corpse[i].SetActive(false); } } }
                //if (collision.gameObject.CompareTag("Player"))
                {
                    //floor1.SetActive(false);
                    //floor2.SetActive(true);

                    GameObject[] corpse = (GameObject[])FindObjectsOfType(typeof(GameObject));
                    for (int i = 0; i < corpse.Length; i++)
                    {
                        if (corpse[i].name.Contains("Death") || corpse[i].name.Contains("Drop") || corpse[i].name.Contains("footprint1"))
                        {
                            corpse[i].SetActive(false);
                        }
                    }
                    GameObject[] oldRooms = (GameObject[])FindObjectsOfType(typeof(GameObject));
                    for (int i = 0; i < oldRooms.Length; i++)
                    {
                        if ((oldRooms[i].name.Contains("Room") && oldRooms[i].transform.childCount == 0))
                        {
                            Destroy(oldRooms[i]);
                        }
                    }


                }
            }
        }


        if (scene.name != "MultiplayerEndless")
        {
            if (collision.gameObject.tag == "Player")
            {
                if (nextFloor == true)
                {
                    if (levelController.GetComponent<LevelController>().currentFloor + 1 != levelController.GetComponent<LevelController>().floors.Length)
                    {
                        levelController.GetComponent<LevelController>().floors[currentFloor].SetActive(false);
                    }
                    else
                    {
                        levelController.GetComponent<LevelController>().currentFloorObject = levelController.GetComponent<LevelController>().floors[currentFloor];
                    }
                    currentFloor++;
                    levelController.GetComponent<LevelController>().currentFloor++;

                    if (levelController.GetComponent<LevelController>().currentFloor == levelController.GetComponent<LevelController>().floors.Length)
                    {
                    }
                    else if (currentFloor <= levelController.GetComponent<LevelController>().floors.Length)
                    {
                        levelController.GetComponent<LevelController>().floors[currentFloor].SetActive(true);
                    }
                    levelController.GetComponent<LevelController>().levelsCleared++;
                }

                //Ruoriksen koodi

                //if (collision.gameObject.CompareTag("Player")) { floor1.SetActive(false); floor2.SetActive(true); GameObject[] corpse = (GameObject[])FindObjectsOfType(typeof(GameObject)); for (int i = 0; i < corpse.Length; i++) { if (corpse[i].name.Contains("Death") || corpse[i].name.Contains("Drop")) { corpse[i].SetActive(false); } } }
                //if (collision.gameObject.CompareTag("Player"))
                {
                    //floor1.SetActive(false);
                    //floor2.SetActive(true);

                    GameObject[] corpse = (GameObject[])FindObjectsOfType(typeof(GameObject));
                    for (int i = 0; i < corpse.Length; i++)
                    {
                        if (corpse[i].name.Contains("Death") || corpse[i].name.Contains("Drop") || corpse[i].name.Contains("footprint1"))
                        {
                            corpse[i].SetActive(false);
                        }
                    }
                    GameObject[] oldRooms = (GameObject[])FindObjectsOfType(typeof(GameObject));
                    for (int i = 0; i < oldRooms.Length; i++)
                    {
                        if ((oldRooms[i].name.Contains("Room") && oldRooms[i].transform.childCount == 0))
                        {
                            Destroy(oldRooms[i]);
                        }
                    }
                }
            }
        }
    }
}

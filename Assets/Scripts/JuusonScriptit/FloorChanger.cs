using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public GameObject floor1, floor2;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            floor1.SetActive(false);
            floor2.SetActive(true);

            GameObject[] corpse = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < corpse.Length; i++)
            {
                if (corpse[i].name.Contains("Death") || corpse[i].name.Contains("Drop"))
                {
                    corpse[i].SetActive(false);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scientist : MonoBehaviour
{   
    GameObject[] buffs=new GameObject[3];
    private int buffToActivate;

    void Start()
    {
     buffs[0] =  GameObject.Find("Misc stuff/Canvas/ScientistPanel/SpeedBoost");
     buffs[1] = GameObject.Find("Misc stuff/Canvas/ScientistPanel/VisionBoost");
     buffs[2] = GameObject.Find("Misc stuff/Canvas/ScientistPanel/AmmoBoost");
        if (GameStatus.status.ammobuffsCollected == 3)
        {
            buffToActivate = Random.Range(0, 2);
        }
        else
        {
             buffToActivate = Random.Range(0, 3);
        }
     
     buffs[buffToActivate].SetActive(true);
    }

   
    public void Doping()
    {
        Time.timeScale = 0.0001f;
        GameObject scientistPanel = GameObject.Find("Misc stuff/Canvas/ScientistPanel");
        scientistPanel.SetActive(true);
    }
}

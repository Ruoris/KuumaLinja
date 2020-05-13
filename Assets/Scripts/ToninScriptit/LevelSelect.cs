using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{

  private GameObject [] levels= new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        levels[0] = GameObject.Find("Misc stuff/Canvas/Level Selection/Panel/Level2Button");
        levels[1] = GameObject.Find("Misc stuff/Canvas/Level Selection/Panel/Level3Button");
        //levels[2] = GameObject.Find("Misc stuff/Canvas/Level Selection/Panel/Level4Button");
    }

    public void Shower()
    {
        if (GameStatus.status.Level1)
        {
            levels[0].SetActive(true);
        }
        if (GameStatus.status.Level2)
        {
            levels[1].SetActive(true);
        }
        //if (GameStatus.status.Level3)
        //{
        //    levels[2].SetActive(true);
        //}
        if (GameStatus.status.Level1==false)
        {
            levels[0].SetActive(false);
        }
        if (GameStatus.status.Level2 == false)
        {
            levels[1].SetActive(false);
        }
        //if (GameStatus.status.Level3 == false)
        //{
        //    levels[2].SetActive(false);
        //}
    }
    void Update()
    {
        Shower();
    }
}

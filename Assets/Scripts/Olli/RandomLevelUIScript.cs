using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RandomLevelUIScript : MonoBehaviour
{

    public Text enemyScore;
    public GameObject levelConroller;

    public Text timerLabel;
    public Text playerPoints;
    public Text floorText;

    public float time;
    public float prevTime;
    public int prevEnemies;
    // Start is called before the first frame update
    void Start()
    {
        levelConroller = GameObject.FindGameObjectWithTag("LevelController");
    }

    // Update is called once per frame
    void Update()
    {
        enemyScore.text = "" + levelConroller.GetComponent<LevelController>().enemiesKilled;

        if (levelConroller.GetComponent<LevelController>().addPoints == true)
        {
            levelConroller.GetComponent<LevelController>().addPoints = false;
            levelConroller.GetComponent<LevelController>().scorePoints += Mathf.Round((100 - (time - prevTime)) * (levelConroller.GetComponent<LevelController>().enemiesKilled - prevEnemies));
            prevTime = time;
            prevEnemies = levelConroller.GetComponent<LevelController>().enemiesKilled;
        }
        playerPoints.text = "Points: " + levelConroller.GetComponent<LevelController>().scorePoints;
        floorText.text = "Floor: " + levelConroller.GetComponent<LevelController>().levelsCleared;

        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }
}

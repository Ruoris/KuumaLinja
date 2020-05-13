﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class GameStatus : MonoBehaviour
{
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public int currentFloor;
    public static GameStatus status;

    public float score;
    public string currentLevel;

    public int ammobuffsCollected;
    public int ammobuffAmount;
    public int movementSpeedsCollected;
    public float movementSpeedAmount;
    public int radiusIncreasesCollected;
    public float radiusIncreaseAmount;

    public string nextlevel;

    void Start()
    {
        ammobuffsCollected = 0;
        movementSpeedsCollected = 0;
        radiusIncreasesCollected = 0;
        Load();
        Debug.Log("loadattu");
        MovementSpeedSetter();
        AmmobuffSetter();
        RadiusIncreaseSetter();
    }
    // Start is called before the first frame update
    void Awake()
    {
       
        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
            Cursor.visible = true;
        
        
            //GameStatus.status.currentLevel = "";
            //SceneManager.LoadScene("MainMenu");

    }
    public void LevelEnd()
    {
        //Cursor.visible = true;
        Debug.Log("levelComplete");
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex == 2)
        {
           nextlevel = "Level2";
        }
        if (scene.buildIndex == 3)
        {
            GameStatus.status.Level1 = true;
             nextlevel = "Cutscene 2";
        }
        if (scene.buildIndex == 4)
        {
            
           nextlevel = "Level2.1";
        }
        //if (scene.buildIndex == 5)
        //{
        //    GameStatus.status.Level2 = true;
        //     nextlevel = "Cutscene 3";
        //}
        if (scene.buildIndex == 5)
        {
            GameStatus.status.Level2 = true;
            nextlevel = "Boss Scene";
        }
        if (scene.buildIndex == 6)
        {
            GameStatus.status.Level3 = true;
            nextlevel = "Cutscene 3";
        }
        if (scene.buildIndex == 7)
        {
            
            nextlevel = "Credits";
        }  
        if (scene.buildIndex != 7)
        {
        int sceneToLoad = scene.buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
        AudioImmortality.immortal.ChangeBackgroundMusic(nextlevel);
        GameStatus.status.currentLevel = nextlevel;
        Debug.Log(SceneManager.GetActiveScene().name);
        GameStatus.status.Save();
        }
        if(scene.buildIndex == 7)
            {
          
            SceneManager.LoadScene("UIMainmenukehitysScene");
            AudioImmortality.immortal.ChangeBackgroundMusic("UIMainmenukehitysScene");
            GameStatus.status.currentLevel = nextlevel;
            Debug.Log(SceneManager.GetActiveScene().name);
            GameStatus.status.Save();

            GameStatus.status.currentLevel = "UIMainmenukehitysScene";
        }
      
       
    }
    public void MovementSpeedSetter()
    {
        if (movementSpeedsCollected == 0)
        {
            movementSpeedAmount = 0.0f;
        }
        else
        {
           movementSpeedAmount = movementSpeedsCollected /2.00f;
            Debug.Log(movementSpeedAmount);
        }
    }
    public void AmmobuffSetter()
    {
        if (ammobuffsCollected == 0)
        {
           ammobuffAmount = 0;
        }
        if (ammobuffsCollected == 1)
        {
            ammobuffAmount = 1;
        }
        if (ammobuffsCollected == 2)
        {
            ammobuffAmount = 3;
        }
        if (ammobuffsCollected == 3)
        {
            ammobuffAmount = 5;
        }
    }
    public void RadiusIncreaseSetter()
    {
        if (radiusIncreasesCollected == 0)
        {
            radiusIncreaseAmount = 0.0f;
        }
     
        else
        {
            radiusIncreaseAmount= radiusIncreasesCollected / 2.0f;
        }
    }
    public void DeathRestart(int current)
    {
       // currentFloor = current;

        SceneManager.LoadScene(current);      
        Debug.Log("GamesStatus");
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();            
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        data.ammobuffsCollected = ammobuffsCollected;
        data.movementSpeedsCollected = movementSpeedsCollected;
        data.radiusIncreasesCollected = radiusIncreasesCollected;
        bf.Serialize(file, data);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
        
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
            Level4 = data.Level4;
            ammobuffsCollected = data.ammobuffsCollected;
            movementSpeedsCollected = data.movementSpeedsCollected;
            radiusIncreasesCollected = data.radiusIncreasesCollected;
        }


    }
    public void UnLoad()
    {
       
        Level1 = false;
        Level2 = false;
        Level3 = false;
        Level4 = false;
       ammobuffsCollected=0;
       movementSpeedsCollected=0;
       radiusIncreasesCollected=0;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        data.ammobuffsCollected = ammobuffsCollected;
        data.movementSpeedsCollected = movementSpeedsCollected;
        data.radiusIncreasesCollected = radiusIncreasesCollected;
        bf.Serialize(file, data);
        file.Close();
        AmmobuffSetter();
        MovementSpeedSetter();
        RadiusIncreaseSetter();
    }
}

[Serializable]
class PlayerData
{
  

    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public int ammobuffsCollected;
    public int movementSpeedsCollected;    
    public int radiusIncreasesCollected;
    

}


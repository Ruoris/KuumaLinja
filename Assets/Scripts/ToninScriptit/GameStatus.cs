using System.Collections;
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


    void Start()
    {
        Load();
        Debug.Log("loadattu");
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel == "UIMainmenukehitysScene")
        {
            Cursor.visible = true;
        }
        
            //GameStatus.status.currentLevel = "";
            //SceneManager.LoadScene("MainMenu");

    }
    public void DeathRestart(int current)
    {
        currentFloor = current;

        SceneManager.LoadScene(currentLevel);      
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

        }


    }
    public void UnLoad()
    {
       
        Level1 = false;
        Level2 = false;
        Level3 = false;
        Level4 = false;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class PlayerData
{
  

    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;

}



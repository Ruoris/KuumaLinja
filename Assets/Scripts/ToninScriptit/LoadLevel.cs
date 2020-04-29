using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string LevelToLoad;
    public bool cleared;


    void Start()
    {
        //if (GameStatus.status.GetType().GetField(LevelToLoad).GetValue(GameStatus.status).ToString() == "True")
        //{
        //    Cleared(true);
        //}
    }

    // Update is called once per frame
 
    public string LevelName()
    {
        return LevelToLoad;
    }
    public void Loadscene()
    {
        GameStatus.status.currentLevel = LevelToLoad;
        SceneManager.LoadScene(LevelToLoad);
        AudioImmortality.immortal.ChangeBackgroundMusic(LevelToLoad);
    }
    public void Cleared(bool isClear)
    {
        if (isClear == true)
        {
            cleared = true;
            //String nimi boolimuuttujaan Gamestatukses
            GameStatus.status.GetType().GetField(LevelToLoad).SetValue(GameStatus.status, true);

            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}

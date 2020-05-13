using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImmortality : MonoBehaviour
{
    public static AudioImmortality immortal;
    private AudioSource backgroundMusic;
    public AudioClip stageSelection;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip cutScene;
    public AudioClip cutScene2;
    public AudioClip Level3;
    public AudioClip cutScene3;
    public AudioClip cutScene4;
    public AudioClip credits;
    public AudioClip boss;

    void Awake()
    {
        if (immortal == null)
        {
            DontDestroyOnLoad(gameObject);
            immortal = this;

        }
        else { Destroy(gameObject); }
        backgroundMusic = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }
 public void ChangeBackgroundMusic(string sceneName)
    {
       
        if (sceneName == "UIMainmenukehitysScene")   //&& GameStatus.status.currentLevel !=sceneName)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = stageSelection;
            backgroundMusic.Play();
        }
        if (sceneName == "Level2")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = level1;
            backgroundMusic.Play();

        }
        if (sceneName == "Level2.1")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = level1;
            backgroundMusic.Play();

        }
        if (sceneName == "Cutscene")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = cutScene;
            backgroundMusic.Play();

        }
        if (sceneName == "Cutscene 2")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = cutScene2;
            backgroundMusic.Play();

        }
        if (sceneName == "Cutscene 3")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = credits;
            backgroundMusic.Play();

        }
        if (sceneName == "Cutscene 4")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = cutScene4;
            backgroundMusic.Play();
        }
        //if (sceneName == "Credits")
        //{
        //    backgroundMusic.Stop();
        //    backgroundMusic.clip = credits;
        //    backgroundMusic.Play();
        //}
        if (sceneName == "Boss Scene")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = boss;
            backgroundMusic.Play();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImmortality : MonoBehaviour
{
    public static AudioImmortality immortal;
    private AudioSource backgroundMusic;
    public AudioClip stageSelection;
    public AudioClip level1;
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
        if (sceneName == "juusonsceneUIlla")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = level1;
            backgroundMusic.Play();

        }
        
    }
}

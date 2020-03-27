using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImmortality : MonoBehaviour
{
    public static AudioImmortality immortal;


    void Awake()
    {
        if (immortal == null)
        {
            DontDestroyOnLoad(gameObject);
            immortal = this;

        }
        else { Destroy(gameObject); }
    }
    
  
}

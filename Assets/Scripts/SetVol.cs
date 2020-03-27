using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SetVol : MonoBehaviour
{
    
    public AudioMixer musicmixer;
    public void SetLevelAll(float sliderValue)
    {
        musicmixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLeveleffects(float sliderValue)
    {
        musicmixer.SetFloat("EffectsChannel", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelMusic(float sliderValue)
    {
        
        musicmixer.SetFloat("MusicChannel", Mathf.Log10(sliderValue) * 20);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVol : MonoBehaviour
{
    public Image filler;
    public AudioMixer musicmixer;
    public Image handle;
   

    public Slider mainSlider;
   
    public void Start()
    {
       
    }

    
   
    public void SetLevelAll(float sliderValue)
    {
        filler.fillAmount = sliderValue / 1.0000f;
        handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
       
    }
    public void SetLeveleffects(float sliderValue)
    {
        filler.fillAmount = sliderValue / 1.0000f;
        handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("EffectsChannel", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelMusic(float sliderValue)
    {
        filler.fillAmount = sliderValue / 1.0000f;
        handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("MusicChannel", Mathf.Log10(sliderValue) * 20);
    }

}
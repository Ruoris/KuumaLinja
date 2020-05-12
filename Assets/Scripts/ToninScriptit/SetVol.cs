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
    public ParticleSystem master;

    public Slider mainSlider;
    private float previousValue;
    public void Start()
    {
        ////Adds a listener to the main slider and invokes a method when the value changes.
        //mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        previousValue = 1;
    }

    // Invoked when the value of the slider changes.
    //public void ValueChangeCheck()
    //{
    //    if (previousValue > mainSlider.value)
    //    {
    //        Debug.Log("Pienenee");
    //        master.Play();
    //    }
        
    //    previousValue = mainSlider.value;

    //}
    public void Comparer(float sliderValue)
    {
        if (previousValue >sliderValue)
        {
            Debug.Log("Pienenee");
            master.Play();
        }
        previousValue = sliderValue;
    }
    public void SetLevelAll(float sliderValue)
    {
        filler.fillAmount = sliderValue / 1.0000f;
        handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        Comparer(sliderValue);
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
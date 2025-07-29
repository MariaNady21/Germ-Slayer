using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
   

    void Start()
    {
        // Load saved values if needed
        float value;
        if (audioMixer.GetFloat("MasterVolume", out value))
            masterSlider.value = Mathf.Pow(10, value / 20f);


        if (audioMixer.GetFloat("MusicVolume", out value))
            musicSlider.value = Mathf.Pow(10, value / 20f);
        musicSlider.maxValue=musicSlider.value;

        if (audioMixer.GetFloat("SFXVolume", out value))
            sfxSlider.value = Mathf.Pow(10, value / 20f);

        
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }

   
}

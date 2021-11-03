using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public GameObject menuCanvas, optionsCanvas;
    float currentVolume;
    // Start is called before the first frame update
    void Start()
    {      
        LoadSettings();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference",currentVolume); 
        optionsCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
    public void LoadSettings()
{
	if (PlayerPrefs.HasKey("FullscreenPreference"))
		Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
	else
		Screen.fullScreen = true;
	if (PlayerPrefs.HasKey("VolumePreference"))
		volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
	else
		volumeSlider.value = 
PlayerPrefs.GetFloat("VolumePreference");
}
}

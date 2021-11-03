using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    float currentVolume;
    [SerializeField] GameObject pauseMenu = null, pauseMain, player, optionsMenu, normMenu;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
        normMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {  
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
		    pauseMenu.SetActive(isPaused);
            pauseMain.gameObject.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape)&&isPaused)
        {
            Continue();
        }
    }
    public void Continue()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
		pauseMenu.SetActive(isPaused);
    }
    public void Beginning()
    {
        player.transform.position = new Vector3(-4.87f,0.5f,0f);
    }

    public void FanSection()
    {
        player.transform.position = new Vector3(2.91f,61.12f,0f);
    }

    public void WindSection()
    {
        player.transform.position = new Vector3(15.6f, 88.5f,0f);
    }

    public void NearEnd()
    {
        player.transform.position = new Vector3(18.6f,111.2f,0f);
    }
    public void Options()
    {
        normMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
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
        normMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
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

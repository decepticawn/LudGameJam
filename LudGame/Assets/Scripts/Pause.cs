using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null, pauseMain, teleportLoc, player;
    [SerializeField] AudioSource audio;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {  
            audio.Pause();
             teleportLoc.gameObject.SetActive(false);
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
        audio.UnPause();
        teleportLoc.gameObject.SetActive(false);
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
		pauseMenu.SetActive(isPaused);
    }
    public void Teleport()
    {
        pauseMain.gameObject.SetActive(false);
        teleportLoc.gameObject.SetActive(true);
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

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
    public void Back()
    {
        pauseMain.gameObject.SetActive(true);
        teleportLoc.gameObject.SetActive(false);
    }
}

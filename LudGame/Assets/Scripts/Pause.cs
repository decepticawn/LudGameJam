using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null, teleportLoc;
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
             isPaused = !isPaused;
             Time.timeScale = isPaused ? 0 : 1;
		     pauseMenu.SetActive(isPaused);
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
    public void Teleport()
    {

    }
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
    public void Back()
    {

    }
}

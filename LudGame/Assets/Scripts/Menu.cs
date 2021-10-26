using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    private int seconds = 1;
    [SerializeField]private Image img1, img2, img3;
    [SerializeField]private GameObject MainMenu;
    private Scene MainGameScene;
    SavePos playerPosData;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.gameObject.SetActive(true);
        StartCoroutine(FadeImage(true));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Beginning()
    {
        PlayerPosSave(-4.87f,0.5f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    public void FanSection()
    {
        PlayerPosSave(2.91f,61.12f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    public void WindSection()
    {
        PlayerPosSave(15.6f, 88.5f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    public void NearEnd()
    {
        PlayerPosSave(18.6f,111.2f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    IEnumerator FadeImage(bool fadeIn)
    {
        // fade from opaque to transparent
        if (fadeIn)
        {
            // loop over 1 second backwards
            for (float i = 0;i<= seconds; i += Time.deltaTime)
            {
                // set color with i as alpha
                img1.color = new Color(1, 1, 1, i);
                img2.color = new Color(1, 1, 1, i);
                img3.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = seconds;i>= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img1.color = new Color(1, 1, 1, i);
                img2.color = new Color(1, 1, 1, i);
                img3.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    public void PlayerPosSave(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("p_x", x);
        PlayerPrefs.SetFloat("p_y", y);
        PlayerPrefs.SetFloat("p_z", z);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }
}

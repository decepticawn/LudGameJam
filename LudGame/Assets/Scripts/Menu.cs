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
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField]private GameObject MainMenu, optionsMenu, continueMenu;
    [SerializeField]private Button continueButton;
    private Scene MainGameScene;
    SavePos playerPosData;
    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.gameObject.SetActive(false);
        continueMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        //StartCoroutine(FadeImage(true));
        StartCoroutine(FadeCanvasGroup(true));
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("isNewGame") == 1)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        MainMenu.gameObject.SetActive(false);
        continueMenu.gameObject.SetActive(true);
    }

    public void WindSection()
    {
        PlayerPosSave(2.91f,61.12f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }

    public void NearEnd()
    {
        PlayerPosSave(15.6f, 88.5f,0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGameScene");
    }
    public void Options()
    {
        MainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void NewGame()
    {
        PlayerPosSave(-4.87f,0.5f,0f);
        SceneManager.LoadScene("MainGameScene");
    }

    public void Back()
    {
        MainMenu.gameObject.SetActive(true);
        continueMenu.gameObject.SetActive(false);
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
    
    IEnumerator FadeCanvasGroup(bool fadeIn)
    {
        // fade from opaque to transparent
        if (fadeIn)
        {
            // loop over 1 second backwards
            for (float i = 0;i<= seconds; i += Time.deltaTime)
            {
                // set color with i as alpha
                canvasGroup.alpha = i;
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
                canvasGroup.alpha = i;
                yield return null;
            }
        }
    }

    public void PlayerPosSave(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("p_x", x);
        PlayerPrefs.SetFloat("p_y", y);
        PlayerPrefs.SetFloat("p_z", z);
        PlayerPrefs.SetInt("isNewGame", 0);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }
}

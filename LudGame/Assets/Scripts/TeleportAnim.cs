using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportAnim : MonoBehaviour
{
    private int seconds = 1;
    private bool isDone = false;
     [SerializeField] private CanvasGroup canvasGroup;
      [SerializeField]private GameObject MainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSetActive());
        MainCanvas.gameObject.SetActive(false);
        StartCoroutine(FadeCanvasGroup(true));
    }

    public void Exit()
    {
        SceneManager.LoadScene("MenuScene");
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
            if(isDone == false)
            {
                StartCoroutine(FadeCanvasGroup(false));
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
            StartCoroutine(FadeCanvasGroup(true)); 
        }
    }
    IEnumerator waitSetActive()
    {
        yield return new WaitForSeconds(4);
        isDone = true;
        MainCanvas.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPercentage : MonoBehaviour
{
    public GameObject player;
    public float playerY = 0f;
    public float percent = 0f;
    // Update is called once per frame
    void Update()
    {
        playerY = player.transform.position.y-0.5f;
        percent = (playerY/102)*100;
        PlayerPrefs.SetFloat("percent", percent);
        PlayerPrefs.Save();
    }
}

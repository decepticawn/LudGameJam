using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPercentage : MonoBehaviour
{
    [SerializeField]private Text ProgressDisplay;
    private float percentage;

    // Update is called once per frame
    void Update()
    {
        percentage = PlayerPrefs.GetFloat("percent");
        ProgressDisplay.text = ":("+ string.Format("{0:N2}", percentage)+"%)";
    }
}

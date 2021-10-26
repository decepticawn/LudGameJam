using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congrats : MonoBehaviour
{
    public GameObject CongratsScreen;
    void OnTriggerEnter(Collider col)
    {
        CongratsScreen.gameObject.SetActive(true);
    }
    void OnTriggerExit(Collider col)
    {
        CongratsScreen.gameObject.SetActive(false);
    }
}

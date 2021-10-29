using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Congrats : MonoBehaviour
{
    [SerializeField]private GameObject PressEScreen;
    [SerializeField]private Collider portalBox;
    private bool isByPortal = false;
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            PressEScreen.gameObject.SetActive(true);
            isByPortal = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            PressEScreen.gameObject.SetActive(false);
            isByPortal = false;
        }
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.E)&&isByPortal)
        {
            SceneManager.LoadScene("Teleport+WinScene");
        }
    }
}

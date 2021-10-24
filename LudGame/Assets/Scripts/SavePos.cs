using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavePos : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float pX;
    private float pY;
    private float pZ;
    private Scene ArtScene;
    
    // Start is called before the first frame update
    void Start()
    {
        //loadArtScene();
        if(PlayerPrefs.GetInt("Saved")==1&&PlayerPrefs.GetInt("TimeToLoad")==1)
        {
            float pX = player.transform.position.x;
            float pY = player.transform.position.y;
            float pZ = player.transform.position.z;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            pZ = PlayerPrefs.GetFloat("p_z");
            player.transform.position = new Vector3(pX, pY, pZ);
            PlayerPrefs.SetInt("TimeToLoad", 0);
            PlayerPrefs.Save();
        }
    }

    public void PlayerPosLoad()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }

    private void loadArtScene()
    {
        SceneManager.LoadScene("ArtScene", LoadSceneMode.Additive);
    }
}

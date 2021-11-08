using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChange : MonoBehaviour
{
    [SerializeField]private SpriteRenderer sr;
    [SerializeField]private Sprite normSprite, pressedSprite;
    [SerializeField]private int keyTypeNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyTypeNum == 1)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                sr.sprite = pressedSprite;
            }
            else if(Input.GetKeyUp(KeyCode.W))
            {
                sr.sprite = normSprite;
            }
        }
        else if(keyTypeNum == 2)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                sr.sprite = pressedSprite;
            }
            else if(Input.GetKeyUp(KeyCode.A))
            {
                sr.sprite = normSprite;
            }
        }
        else if(keyTypeNum == 3)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                sr.sprite = pressedSprite;
            }
            else if(Input.GetKeyUp(KeyCode.S))
            {
                sr.sprite = normSprite;
            }
        }
        else if(keyTypeNum == 4)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                sr.sprite = pressedSprite;
            }
            else if(Input.GetKeyUp(KeyCode.D))
            {
                sr.sprite = normSprite;
            }
        }
        else if(keyTypeNum == 5)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                sr.sprite = pressedSprite;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                sr.sprite = normSprite;
            }
        }
    }
}

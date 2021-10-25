using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : MonoBehaviour
{
    public bool isHeavy = false, canHeavy;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private GameObject cube;
    [SerializeField] private Renderer cubeRender;
    [SerializeField] private Material heavy, def;
    void Start()
    {
        cubeRender = cube.GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))&&canHeavy)
        {
            isHeavy = true;
            playerRB.mass = 300f;
            cubeRender.material = heavy;
        }
        else
        {
            isHeavy = false;
            playerRB.mass = 10f;
            cubeRender.material = def;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public float tapSpeed = 0.5f; //in seconds
    private float lastTapTime = 0;

    // Use this for initialization
    void Start()
    {
        lastTapTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((Time.time - lastTapTime) < tapSpeed)
            {
                Debug.Log("Double tap");
            }
            lastTapTime = Time.time;
        }
    }
}

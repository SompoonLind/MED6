using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwatch : MonoBehaviour
{
    public bool timerActive = false;
    float currentTime;

    void Start()
    {
        currentTime = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive){
            currentTime = currentTime + Time.deltaTime;
        }
        
        float rounded = Mathf.Round(currentTime * 1000.0f) / 1000.0f;

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Looked at box for " + rounded + " seconds");
        }
    }
}

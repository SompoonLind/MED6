using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTimer : MonoBehaviour
{
    private float currentTimer;
    void Start()
    {
        currentTimer = 0;
    }

    public float StartCounter()
    {
        currentTimer = currentTimer + Time.deltaTime;
        float rounded = Mathf.Round(currentTimer * 1000.0f) / 1000.0f;
        return rounded;
    }
}

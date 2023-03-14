using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    float MaxVal;
    [Range(0.0f, 1f)]
    float slider;

    public float slide()
    {
        return slider;
    }
}

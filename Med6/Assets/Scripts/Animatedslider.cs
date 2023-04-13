using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Animatedslider : MonoBehaviour
{
    Slider slider;
    Testparticles viz;

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.FindObjectOfType<Slider>();
        viz = GameObject.FindObjectOfType<Testparticles>();

    }

    // Update is called once per frame
    void Update()
    {
        viz.Max = slider.value;
    }
}

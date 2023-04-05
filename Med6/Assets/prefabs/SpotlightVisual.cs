using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightVisual : MonoBehaviour
{
    public float speed = 1.0f; // Speed of rotation & transform

    private Transform spotlight_transform;

    // Start is called before the first frame update
    void Start()
    {
        spotlight_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        spotlight_transform.Rotate(Vector3.up, speed * Time.deltaTime);

        float move_amount = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        spotlight_transform.Translate(Vector3.up * move_amount);
     }
}

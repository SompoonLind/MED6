using UnityEngine;
using System.Collections;

public class RayCastDetector : MonoBehaviour {
    private Camera cam;
    public float rayDistance = 5f;
    private stopwatch timer;
void Start()
    {
        cam = this.GetComponent<Camera>();
        timer = this.GetComponent<stopwatch>();
    }
 
void Update()
    {
        RaycastHit hit;

        timer.timerActive = false;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.tag == "Finish")
            {
                timer.timerActive = true;
            }
        }
    }
}
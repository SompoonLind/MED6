using UnityEngine;
using System.Collections;

public class RayCastDetector : MonoBehaviour {
    public Camera cam;
    public float rayDistance = 5f;
    public stopwatch timer;
void Start()
    {
        
    }
 
void Update()
    {
        Camera cam = this.cam;
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
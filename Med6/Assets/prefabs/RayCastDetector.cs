using UnityEngine;

public class RayCastDetector : MonoBehaviour {
    private Camera cam;
    public float rayDistance = 5f;
    private float currentTime;

void Start()
    {
        currentTime = 0;
        Cursor.lockState = CursorLockMode.Locked;
        cam = this.GetComponent<Camera>();
    }
 
void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.tag == "Finish")
            {
                currentTime = currentTime + Time.deltaTime;
        
                float rounded = Mathf.Round(currentTime * 1000.0f) / 1000.0f;

                if (Input.GetMouseButton(0))
                {
                    Debug.Log("Looked at " + hit.collider.gameObject.name + " for " + rounded + " seconds");
                }
            }
        }
    }
}

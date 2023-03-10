using UnityEngine;

public class RayCastDetector : MonoBehaviour {
    private Camera cam;
    public float rayDistance = 5f;
    private objectTimer rounded;


void Start()
    {
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
                
                rounded = hit.collider.gameObject.GetComponent<objectTimer>();

                if (Input.GetMouseButton(0))
                {
                    Debug.Log("Looked at " + hit.collider.name + " for " + rounded.StartCounter() + " seconds");
                } 
            }
        }
    }
}

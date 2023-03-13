using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointcloud : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayDistance = 5f;
    private Camera cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = this.GetComponent<Camera>();
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.point);
            //GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Destroy(primitive.GetComponent<Collider>());
            //primitive.transform.position = hit.point;
            //primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //primitive.GetComponent<Renderer>().material = mat;
            

            


        }

    }
}

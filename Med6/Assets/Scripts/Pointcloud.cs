using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Pointcloud : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayDistance = 5f;
    private Camera cam;
    string filename = "";
    bool headerLine = true;
    RaycastHit hit;
    RaycastHit hit2;
    private float currentTime;
    public bool wantReflectance = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = this.GetComponent<Camera>();
        var curTime = "";
        curTime = System.DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
        
        //filename = Application.dataPath + "/CSVFiles/test2.csv";
        filename = Application.dataPath + "/CSV/" + curTime +".csv";
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit)) {
            if (wantReflectance == true){//If developer wants reflectance
                ReflectanceActive();//Rin reflectance script before writing csv
            }
            Debug.Log(hit);
            WriteCSV();
            //Debug.Log(hit.point[0]);
            //GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Destroy(primitive.GetComponent<Collider>());
            //primitive.transform.position = hit.point;
            //primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //primitive.GetComponent<Renderer>().material = mat;
        }


    }
        public void ReflectanceActive(){
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("reflective")) {//See if layer hit by ray is reflective
                Vector3 inDirection = Vector3.Reflect(transform.forward,hit.normal);
                Ray ray2 = new Ray(hit.point, inDirection);// Create a new ray in direction 
                Debug.DrawRay (hit.point, inDirection * rayDistance, Color.blue);//visualize new ray
                if (Physics.Raycast(ray2, out hit2)) {//if new ray hits 
                    hit = hit2;//Replace original hit with hit2
                }
            }
        }


        public void WriteCSV()
        {
        
        TextWriter tw = new StreamWriter(filename, true);

        currentTime += Time.deltaTime;

        if (headerLine == true)
        {
            tw.WriteLine("X, Y, Z, Time"); //Add to this list if we want to add more predetermined things
            tw.Close();
            tw = new StreamWriter(filename, true);
            headerLine = false;
        }
            
        for (int i = 0; i < 1; i++)
        {
            tw.WriteLine(Mathf.Round(hit.point.x * 1000.0f) / 1000.0f + ";" + Mathf.Round(hit.point.y * 1000.0f) / 1000.0f + ";" + Mathf.Round(hit.point.z * 1000.0f) / 1000.0f + ";" + Mathf.Round(currentTime * 100.0f) / 100.0f); //Add to this list if we want to add more predetermined things
        }
   
        tw.Close();
        
    }
}


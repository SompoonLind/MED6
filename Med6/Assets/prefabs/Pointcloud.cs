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
            WriteCSV();
            //Debug.Log(hit.point[0]);
            //GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Destroy(primitive.GetComponent<Collider>());
            //primitive.transform.position = hit.point;
            //primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //primitive.GetComponent<Renderer>().material = mat;
        }


    }
        public void WriteCSV()
        {
        
        TextWriter tw = new StreamWriter(filename, true);

        if (headerLine == true)
        {
            tw.WriteLine("X, Y, Z"); //Add to this list if we want to add more predetermined things
            tw.Close();
            tw = new StreamWriter(filename, true);
            headerLine = false;
        }
            
        for (int i = 0; i < 1; i++)
        {
            tw.WriteLine(Mathf.Round(hit.point.x * 1000.0f) / 1000.0f + ";" + Mathf.Round(hit.point.y * 1000.0f) / 1000.0f + ";" + Mathf.Round(hit.point.z * 1000.0f) / 1000.0f); //Add to this list if we want to add more predetermined things
        }
   
        tw.Close();
        
    }
}


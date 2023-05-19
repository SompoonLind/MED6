using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public float rayDistance = 5f;
    private Camera cam;
    string filename = "";
    float currentTime;
    bool headerLine = true;
    RaycastHit hit;
    RaycastHit hit2;
    public bool wantReflectance = false;
    float Px;
    float Py;
    float Pz;
    float Rx;
    float Ry;
    float Rz;
    float Rw;
    void Awake()
    {
        var curTime = "";
        curTime = System.DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
        string filePath = Application.dataPath + "/CSV/";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        filename = Application.dataPath + "/CSV/" + curTime +".csv";
        cam = this.GetComponent<Camera>();
    }

        void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        Px = cam.transform.position.x;
        Py = cam.transform.position.y;
        Pz = cam.transform.position.z;
        Rx = cam.transform.rotation.x;
        Ry = cam.transform.rotation.y;
        Rz = cam.transform.rotation.z;
        Rw = cam.transform.rotation.w;

        if (Physics.Raycast(ray, out hit)) {
            if (wantReflectance == true){//If developer wants reflectance
                ReflectanceActive();//Run reflectance script before writing csv
            }
            
        }
        WriteCSV();

    }
    
    public void WriteCSV()
    {
        currentTime += Time.deltaTime;
        TextWriter tw = new StreamWriter(filename, true);

        if (headerLine == true)
        {
            tw.WriteLine("HitX;HitY;HitZ;Px;Py;Pz;Rx;Ry;Rz;Rw;Time"); //Add to this list if we want to add more predetermined things
            tw.Close();
            tw = new StreamWriter(filename, true);
            headerLine = false;
        }
            
        for (int i = 0; i < 1; i++)
        {
            tw.WriteLine(Mathf.Round(hit.point.x * 1000.0f) / 1000.0f + ";" + Mathf.Round(hit.point.y * 1000.0f) / 1000.0f 
            + ";" + Mathf.Round(hit.point.z * 1000.0f) / 1000.0f + ";" + 
            Px + ";" + Py + ";" + Pz  + ";" + Rx+ ";"
             + Ry + ";" + Rz  + ";" + Rw  + ";" + currentTime); //Add to this list if we want to add more predetermined things
        }

        tw.Close();
        
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
}

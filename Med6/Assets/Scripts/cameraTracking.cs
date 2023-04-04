using UnityEngine;
using System.IO;

public class cameraTracking : MonoBehaviour
{
    string filename = "";
    float currentTime;
    float Px;
    float Py;
    float Pz;
    float Rx;
    float Ry;
    float Rz;
    float Rw;
    Camera cam;
    void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        var curTime = "";
        curTime = System.DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
        filename = Application.dataPath + "/CSV/" + curTime +".csv";
    }

    void Update()
    {
        Px = cam.transform.position.x;
        Py = cam.transform.position.y;
        Pz = cam.transform.position.z;
        Rx = cam.transform.rotation.x;
        Ry = cam.transform.rotation.y;
        Rz = cam.transform.rotation.z;
        Rw = cam.transform.rotation.w;
        WriteCSV();
    }

    bool headerLine = true;
    void WriteCSV()
    {
        currentTime += Time.deltaTime;
        TextWriter tw = new StreamWriter(filename, true);

        if (headerLine == true)
        {
            tw.WriteLine("Px, Py, Pz, Rx, Ry, Rz, Rw, Time"); //Add to this list if we want to add more predetermined things
            tw.Close();
            tw = new StreamWriter(filename, true);
            headerLine = false;
        }
            
        for (int i = 0; i < 1; i++)
        {
            tw.WriteLine(Px + ";" + Py + ";" + Pz + ";" + Rx + ";" + Ry + ";" + Rz + ";" + Rw + ";" + currentTime); //Add to this list if we want to add more predetermined things
        }
   
        tw.Close();
        
    }
}

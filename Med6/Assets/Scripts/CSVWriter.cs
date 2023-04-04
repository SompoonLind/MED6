using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";
    float currentTime;

    void Awake()
    {
        var curTime = "";
        curTime = System.DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
        filename = Application.dataPath + "/CSVFiles/" + curTime +".csv";
    }

    bool headerLine = true;
    public void WriteCSV(float Px, float  Py, float Pz, float Rx, float Ry, float Rz)
    {
        currentTime += Time.deltaTime;
        TextWriter tw = new StreamWriter(filename, true);

        if (headerLine == true)
        {
            tw.WriteLine("Px, Py, Pz, Rx, Ry, Rz, Time"); //Add to this list if we want to add more predetermined things
            tw.Close();
            tw = new StreamWriter(filename, true);
            headerLine = false;
        }
            
        for (int i = 0; i < 1; i++)
        {
            tw.WriteLine(Px + ";" + Py + ";" + Pz + ";" + Rx + ";" + Ry + ";" + Rz + ":" + currentTime); //Add to this list if we want to add more predetermined things
        }
   
        tw.Close();
        
    }
}

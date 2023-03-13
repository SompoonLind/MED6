/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR;
using System.Linq;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";

    [System.Serializable]
    public class User
    {
        public string name; //Add to this list if we want to add more predetermined things
        public float position;
        public float runTime;
        public float guardianTime;
    }

    //public string filePath = Application.dataPath + "/test2.csv";

    void Start()
    {
        var curTime = "";
        curTime = System.DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
        
        //filename = Application.dataPath + "/CSVFiles/test2.csv";
        filename = Application.dataPath + "/CSVFiles/" + curTime +".csv";
    }

    void Update()
    {
        WriteCSV();
    }

    bool headerLine = true;
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
            tw.WriteLine(hit.point[0] + ";" + hit.point[1] + ";" + hit.point[2]); //Add to this list if we want to add more predetermined things
        }
   
        tw.Close();
        
    }
}
*/
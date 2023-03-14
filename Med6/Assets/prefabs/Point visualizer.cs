using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PointVisualizer : MonoBehaviour
{
    public float Slider;
    string filePath;
    StreamReader reader;
    string[] valuesRead;
    List<float> timeValues = new List<float>();
    bool alreadyRead = true;
    string[][] data;


    void Start()
    {
        filePath = Application.dataPath + "/CSV/test.csv";
        reader = new StreamReader(filePath);

        reader.ReadLine();

        // Read the rest of the lines and split values
        string[] lines = reader.ReadToEnd().Split("\n"[0]);
        data = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++) {
            data[i] = lines[i].Split(";"[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < data.Length; i++)
        {
            
            timeValues.Add(float.Parse(data[i][3]));
        }
        if (alreadyRead)
        {
            int maxVal = timeValues.Count;
            Debug.Log(maxVal);
        }
        alreadyRead = false;


    }

}

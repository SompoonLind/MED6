using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PointVisualizer : MonoBehaviour
{
    float endTime;
    [Range(0.0f, 1.0f)]
    public float Slider;
    string filePath;
    StreamReader reader;
    string[] values;

    void Start()
    {
        filePath = Application.dataPath + "/CSV/test.csv";
        reader = new StreamReader(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        string line;
        float normalizedTime;
        float endTime;
        bool removeLine = true;
        List<string> valuesDeluxe = new List<string>();

        while(!reader.EndOfStream)
        {
            line = reader.ReadLine();
            values = line.Split(";");
            for (int i = 0; i < 4; i++)
            {
                valuesDeluxe.Add(values[i]);
                if (removeLine)
                {
                    valuesDeluxe.RemoveAt(0);
                    removeLine = false;
                }
                string finalvalues = string.Join(";", values);
                valuesDeluxe.Add(finalvalues);
            }
            valuesDeluxe[0].Split(";");
            //Debug.Log(valuesDeluxe.Count);
            Debug.Log(valuesDeluxe[2]);
        }
/*
        endTime = float.Parse(values[3]);
        normalizedTime = float.Parse(values[3]) / endTime;

        if (normalizedTime > 6)
        {
            GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            primitive.GetComponent<MeshRenderer>().material.color = Color.green;
            Destroy(primitive.GetComponent<Collider>());   
            Vector3 spherePos = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            primitive.transform.position = spherePos;
            primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);                
        }
        */
    }

}

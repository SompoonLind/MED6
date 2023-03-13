using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Pointvisualizer : MonoBehaviour
{
    string filePath = Application.dataPath + "/CSV/test.csv";
    StreamReader reader;

    void Start()
    {
        reader = new StreamReader(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        while(!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(";");
            GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(primitive.GetComponent<Collider>());
            Debug.Log(values);
            //primitive.transform.position = values;
            //primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

    }
}

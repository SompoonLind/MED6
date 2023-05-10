using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    [Range(-0.3f, 0.3f)]
    public float Speed = 0; 
    public bool pause;
    public Slider mainSlider;
    string[][] data;
    List<float> XPosValues = new List<float>(); //Liste til alle X-værdier
    List<float> YPosValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZPosValues = new List<float>(); //Liste til alle Z-værdier
    List<Vector3> posValues = new List<Vector3>();
    List<float> XRotValues = new List<float>(); //Liste til alle X-værdier
    List<float> YRotValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZRotValues = new List<float>(); //Liste til alle Z-værdier
    List<float> WRotValues = new List<float>(); //Liste til alle Z-værdier
    List<float> timeVals = new List<float>();
    List<Quaternion> rotValues = new List<Quaternion>();
    public Camera cam;
    CSVReader CSVData;
    GameObject Visualizer;
    void Start()
    {
        Visualizer = GameObject.FindGameObjectWithTag("visualizer");
        CSVData = Visualizer.GetComponent<CSVReader>();
        cam = GameObject.FindObjectOfType<Camera>();
        data = CSVData.datavals();


        for (int i = 0; i < data.Length-1; i++) //For loop der opdeler værdierne i hver ders liste frem for et 2d array
        {
            XPosValues.Add(float.Parse(data[i][3]));
            YPosValues.Add(float.Parse(data[i][4]));
            ZPosValues.Add(float.Parse(data[i][5]));
            posValues.Add(new Vector3(XPosValues[i], YPosValues[i], ZPosValues[i]));
            XRotValues.Add(float.Parse(data[i][6]));
            YRotValues.Add(float.Parse(data[i][7]));
            ZRotValues.Add(float.Parse(data[i][8]));
            WRotValues.Add(float.Parse(data[i][9]));
            rotValues.Add(new Quaternion(XRotValues[i], YRotValues[i], ZRotValues[i], WRotValues[i]));
            timeVals.Add(float.Parse(data[i][10]));
        }
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        for (int i = 0; i < timeVals.Count-2; i++)
        {
            mainSlider.maxValue = timeVals.Count;
            mainSlider.value = i;
            float timeDifference = timeVals[i+1] - timeVals[i];
            cam.transform.position = new Vector3(posValues[i][0], posValues[i][1], posValues[i][2]);
            cam.transform.rotation = rotValues[i];
            while (pause)
            {
                yield return null;
            }
            yield return new WaitForSeconds(timeDifference - Speed);
        }

    }
}

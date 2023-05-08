using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class CSVReader : MonoBehaviour
{
    float Min;
    float Max;
    [SerializeField] private Object CSVFile;
    string filePath; //Fil placering
    StreamReader reader; //Læs fil
    string[][] data; //2d Array til at splitte alle linjer i CSV filen til individuelle lister for X, Y, Z og tid
    List<Vector3> XYZValues = new List<Vector3>(); 
    List<Vector3> XYZValuesRaw = new List<Vector3>(); 
    List<float> XValues = new List<float>(); //Liste til alle X-værdier
    List<float> YValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZValues = new List<float>(); //Liste til alle Z-værdier
    List<float> timeValues = new List<float>(); //Liste til alle tid værdier
    List<float> normalizedTime = new List<float>();
    public void Start()
    {
        string CSVfilePath = AssetDatabase.GetAssetPath(CSVFile);
        CSVfilePath.Replace("\\", "/");
        filePath = CSVfilePath; //Filplacering af CSV fil, skal gøres lidt mere modulært
        reader = new StreamReader(CSVfilePath); //Læs fil på filplacering

        reader.ReadLine(); //Skip header linjen
        reader.ReadLine();

        string[] lines = reader.ReadToEnd().Split("\n"[0]); //Læs alle linjer til et array og split ved newline
        data = new string[lines.Length][]; //2d array med antal linjer i CSV filen og værdien på hver af dem
        for (int i = 0; i < lines.Length; i++) { 
            data[i] = lines[i].Split(";"[0]); //For loop der splitter værdierne ved semikolon, så vi får de individuelle
        }
        
        for (int i = 0; i < data.Length-1; i++) //For loop der opdeler værdierne i hver ders liste frem for et 2d array
        {
            XValues.Add(float.Parse(data[i][0]));
            YValues.Add(float.Parse(data[i][1]));
            ZValues.Add(float.Parse(data[i][2]));
            XYZValuesRaw.Add(new Vector3(XValues[i], YValues[i], ZValues[i]));
            timeValues.Add(float.Parse(data[i][10]));            
        }
    
        XYZValues = XYZValuesRaw.Distinct().ToList();
        float timeValMax = timeValues[XYZValues.Count()];

        for (int i = 0; i < XYZValues.Count; i++)
        {
            float normalized = timeValues[i]/timeValMax;
            normalizedTime.Add(normalized);
        }

        int normalizedTimeCount = normalizedTime.Count()-1;

        Min = normalizedTime[0]; //Sætter minimum værdien fra CSV filen 
        Max = normalizedTime[normalizedTimeCount]; //Sætter maksimum værdien fra CSV filen 
    }

    public List <float> timeVals()
    {
        return normalizedTime;
    }
    int curentnumber = 1;
    public float countXYdoubles()
    {
        int doubles = 1;
        
        for (int i = 0; i < XYZValuesRaw.Count; i++)
        {
            if (XYZValuesRaw[i][0] == XYZValuesRaw[i][2])
            {
                
            }
        }
        
        
        return doubles;
    }
    public List<Vector3> XYZvals()
    {
        return XYZValues;
    }

    public float Minval()
    {
        return Min; 
    }

    public float Maxval()
    {
        return Max; 
    }

    public string[][] datavals()
    {
        return data;
    }

}

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
How to use: ved kørsel af programmet finder den selv min og maks, sætter dem i inspectoren, og tegner derefter den fulde "visualization". 
Hvis man kun vil se en given tidsperiode, skriver man først sin nye ønskede min og max værdi, og trykker derefter på "Reset spheres" checkboxen
*/
public class ParticleTest : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float Min; 
    [Range(0.0f, 1.0f)]
    public float Max;
    float currentMin;
    float currentMax;
    string filePath; //Fil placering
    StreamReader reader; //Læs fil
    string[][] data; //2d Array til at splitte alle linjer i CSV filen til individuelle lister for X, Y, Z og tid
    List<Vector3> XYZvaluesRaw = new List<Vector3>();
    List<float> XValues = new List<float>(); //Liste til alle X-værdier
    List<float> YValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZValues = new List<float>(); //Liste til alle Z-værdier
    List<float> timeValues = new List<float>(); //Liste til alle tid værdier
    List<float> normalizedTime = new List<float>();
    bool spheresDrawn = false; //Bolean så spheres kun tegnes en gang
    GameObject SphereController; //GameObject der sættes som parent for alle spawnede spheres

    void Start()
    {
        SphereController = new GameObject("Sphere Controller"); //Opretter GameObject til at Parente alle spheres, bare så det ser lidt pænere ud i Hierarchy
        filePath = Application.dataPath + "/CSV/test.csv"; //Filplacering af CSV fil, skal gøres lidt mere modulært
        reader = new StreamReader(filePath); //Læs fil på filplacering

        reader.ReadLine(); //Skip header linjen

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
            XYZvaluesRaw.Add(new Vector3(XValues[i], YValues[i], ZValues[i]));
            timeValues.Add(float.Parse(data[i][3]));         
        }

        float timeValMax = timeValues.Last();

        for (int i = 0; i < timeValues.Count; i++)
        {
            float normalized = timeValues[i]/timeValMax;
            normalizedTime.Add(normalized);
        }

        List<Vector3> XYZvalues = XYZvaluesRaw.Distinct().ToList();

        int normalizedTimeCount = normalizedTime.Count()-1;

        Min = normalizedTime[0]; //Sætter minimum værdien fra CSV filen 
        Max = normalizedTime[normalizedTimeCount]; //Sætter maksimum værdien fra CSV filen 
    }

    void Update()
    {
        //Hvis der ikke er tegnet spheres, tegn dem, hvis der bliver trykket reset, destroy dem og tegn dem igen
        if (spheresDrawn == false)
        {
            drawSpheres();
            currentMin = Min;
            currentMax = Max;
        }

        if (currentMin != Min || currentMax != Max)
        {
            Destroy();
        }

    }

    void drawSpheres()
    {
        for (int i = 0; i < XYZvaluesRaw.Count; i++) //For loop på data.length-1 så vi sætter værdien for hver individuel koordinat
        {
            if (normalizedTime[i] > Min && normalizedTime[i] < Max) //Tegn kun dem hvis tid falder indenfor min og max
            {
                //Opretter, tagger, farver og positionerer spheres ud fra CSV data
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Cube); 
                primitive.transform.tag = "visualizer";
                primitive.transform.parent = SphereController.gameObject.transform;
                primitive.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                Destroy(primitive.GetComponent<Collider>());
                primitive.transform.position = XYZvaluesRaw[i];
                primitive.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        spheresDrawn = true;
    }

    void Destroy() //Finder alle gameobjects med Tagget "visualizer" og destroyer dem, og tegner derefter nye
    {
        GameObject[] destroyTag;
        destroyTag = GameObject.FindGameObjectsWithTag("visualizer");
        foreach (GameObject destroy in destroyTag)
        {
            Destroy(destroy);
        }
        spheresDrawn = false;
    }
}
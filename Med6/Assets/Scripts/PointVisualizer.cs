using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
How to use: ved kørsel af programmet finder den selv min og maks, sætter dem i inspectoren, og tegner derefter den fulde "visualization". 
Hvis man kun vil se en given tidsperiode, skriver man først sin nye ønskede min og max værdi, og trykker derefter på "Reset spheres" checkboxen
*/
public class PointVisualizer : MonoBehaviour
{
    GameObject Visualizer;
    CSVReader CSVData;
    [Range(0.0f, 1.0f)]
    public float Min; 
    [Range(0.0f, 1.0f)]
    public float Max;
    float currentMin;
    float currentMax;
    List<float> timeValues = new List<float>();
    List<Vector3> XYZValues = new List<Vector3>();
    bool cubesDrawn = false; //Bolean så spheres kun tegnes en gang
    GameObject CubeController; //GameObject der sættes som parent for alle spawnede cubes 

    void Start()
    {
        CubeController = new GameObject("Cube controller");
        Visualizer = GameObject.FindGameObjectWithTag("visualizer");
        CSVData = Visualizer.GetComponent<CSVReader>();
        CSVData.Start();
        timeValues = CSVData.timeVals();
        XYZValues = CSVData.XYZvals();
        Min = CSVData.Minval();
        Max = CSVData.Maxval();
    }
    void Update()
    {
        //Hvis der ikke er tegnet spheres, tegn dem, hvis der bliver trykket reset, destroy dem og tegn dem igen
        if (cubesDrawn == false)
        {
            drawCubes();
            currentMin = Min;
            currentMax = Max;
        }

        if (currentMin != Min || currentMax != Max)
        {
            Destroy();
        }
    }

    void drawCubes()
    {
        for (int i = 0; i < XYZValues.Count; i++) //For loop på data.length-1 så vi sætter værdien for hver individuel koordinat
        {
            if (timeValues[i] > Min && timeValues[i] < Max) //Tegn kun dem hvis tid falder indenfor min og max
            {
                //Opretter, tagger, farver og positionerer spheres ud fra CSV data
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Cube); 
                primitive.transform.tag = "visual";
                primitive.transform.parent = CubeController.gameObject.transform;
                primitive.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                Destroy(primitive.GetComponent<Collider>());
                primitive.transform.position = XYZValues[i];
                primitive.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
        cubesDrawn = true;
    }

    void Destroy() //Finder alle gameobjects med Tagget "visualizer" og destroyer dem, og tegner derefter nye
    {
        GameObject[] destroyTag;
        destroyTag = GameObject.FindGameObjectsWithTag("visual");
        foreach (GameObject destroy in destroyTag)
        {
            Destroy(destroy);
        }
        cubesDrawn = false;
    }
}

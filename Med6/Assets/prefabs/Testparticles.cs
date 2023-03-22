using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Testparticles : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float Min; 
    [Range(0.0f, 1.0f)]
    public float Max;
    float currentMin;
    float currentMax;
    string filePath; //Fil placering
    StreamReader reader; //Læs fil
    private ParticleSystem particleController;
    string[][] data; //2d Array til at splitte alle linjer i CSV filen til individuelle lister for X, Y, Z og tid
    List<float> XValues = new List<float>(); //Liste til alle X-værdier
    List<float> YValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZValues = new List<float>(); //Liste til alle Z-værdier
    List<float> timeValues = new List<float>(); //Liste til alle tid værdier
    List<Vector3> XYZValuesRaw = new List<Vector3>();
    List<Vector3> XYZValues = new List<Vector3>();
    List<float> normalizedTime = new List<float>();
    private ParticleSystem.Particle[] particles;
    private bool particlesDrawn;

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "/CSV/test.csv"; //Filplacering af CSV fil, skal gøres lidt mere modulært
        reader = new StreamReader(filePath); //Læs fil på filplacering

        reader.ReadLine(); //Skip header linjen

        string[] lines = reader.ReadToEnd().Split("\n"[0]); //Læs alle linjer til et array og split ved newline
        data = new string[lines.Length][]; //2d array med antal linjer i CSV filen og værdien på hver af dem
        for (int i = 0; i < lines.Length; i++)
        {
            data[i] = lines[i].Split(";"[0]); //For loop der splitter værdierne ved semikolon, så vi får de individuelle
        }
        for (int i = 0; i < data.Length - 1; i++) //For loop der opdeler værdierne i hver ders liste frem for et 2d array
        {
            XValues.Add(float.Parse(data[i][0]));
            YValues.Add(float.Parse(data[i][1]));
            ZValues.Add(float.Parse(data[i][2]));
            timeValues.Add(float.Parse(data[i][3]));
            XYZValuesRaw.Add(new Vector3(XValues[i], YValues[i], ZValues[i]));
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

        particleController = this.GetComponent<ParticleSystem>();

        particles = new ParticleSystem.Particle[XYZValues.Count];

        particleController.SetParticles(particles, particles.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!particlesDrawn)
        {
            drawParticles();
            currentMin = Min;
            currentMax = Max;
        }

        if (currentMin != Min || currentMax != Max)
        {
            Destroy();
        }
    }

    void drawParticles()
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.startSize = 1f;

        // Emit all the particles at once
        for (int i = 0; i < XYZValues.Count; i++)
        {
            if (normalizedTime[i] > Min && normalizedTime[i] < Max)
            {
                particleController.Emit(XYZValues.Count);
                // Get the current particles from the ParticleSystem
                int numParticlesAlive = particleController.GetParticles(particles);

                // Set the position of each particle to the corresponding XYZ value
                for (int j = 0; j < numParticlesAlive; j++)
                {
                        particles[j].position = XYZValues[j];
                }
                // Set the modified particles back to the ParticleSystem
                particleController.SetParticles(particles, numParticlesAlive);
                particlesDrawn = true;
            }
        }
    }

    void Destroy()
    {
        particleController.Clear();
        particlesDrawn = false;
    }
}
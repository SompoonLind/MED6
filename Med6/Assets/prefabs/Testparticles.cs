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
    [SerializeField] private ParticleSystem particleSystem;
    string[][] data; //2d Array til at splitte alle linjer i CSV filen til individuelle lister for X, Y, Z og tid
    List<float> XValues = new List<float>(); //Liste til alle X-værdier
    List<float> YValues = new List<float>(); //Liste til alle Y-værdier
    List<float> ZValues = new List<float>(); //Liste til alle Z-værdier
    List<float> timeValues = new List<float>(); //Liste til alle tid værdier
    List<Vector3> XYZValues = new List<Vector3>();
    private ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
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
            timeValues.Add(float.Parse(data[i][3]));    
            XYZValues.Add(new Vector3( XValues[i],ZValues[i],YValues[i]));
        }
        
        particles = new ParticleSystem.Particle[XYZValues.Count];

        for (int i = 0; i < XYZValues.Count; i++)
        {
            particles[i].position = XYZValues[i];
            particles[i].startSize = 100f;
            particles[i].startColor = Color.red;
        }

        particleSystem.SetParticles(particles, particles.Length);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current particles from the ParticleSystem
        int numParticlesAlive = particleSystem.GetParticles(particles);

        // Modify the particle properties
        for (int i = 0; i < numParticlesAlive; i++)
        {
            particles[i].startSize = 1f;
            particles[i].startColor = Color.red;
        }

        // Set the modified particles back to the ParticleSystem
        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
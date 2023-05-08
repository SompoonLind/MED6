using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleVisualizer : MonoBehaviour
{
    GameObject Visualizer;
    CSVReader CSVData;
    [Range(0.0f, 1.0f)]
    public float Min; 
    [Range(0.0f, 1.0f)]
    public float Max;
    float currentMin;
    float currentMax;
    private ParticleSystem particleController;
    private ParticleSystem.Particle[] particles;
    private bool particlesDrawn;
    string[][] data;

    // Start is called before the first frame update
    void Start()
    {
        Visualizer = GameObject.FindGameObjectWithTag("visualizer");
        CSVData = Visualizer.GetComponent<CSVReader>();
        Min = CSVData.Minval();
        Max = CSVData.Maxval();
        data = CSVData.datavals();

        float timeValMax = float.Parse(data[data.Length-2][10]);

        for (int i = 0; i < data.Length-1; i++)
        {
            float normalized = float.Parse(data[i][10])/timeValMax;
            data[i][10] = normalized.ToString();
        }

        particleController = this.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[data.Length];
        particleController.SetParticles(particles, particles.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (particlesDrawn == false)
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

        for (int i = 0; i < data.Length-1; i++)
        {
            float sliderVal = float.Parse(data[i][10]);
            if (sliderVal > Min && sliderVal < Max)
            {
                Vector3 pos = new Vector3(float.Parse(data[i][0]),float.Parse(data[i][1]),float.Parse(data[i][2]));
                var main = particleController.main;
                main.maxParticles = data.Length;
                particleController.Emit(data.Length);
                int numParticlesAlive = particleController.GetParticles(particles);
                particles[i].position = pos;
                particleController.SetParticles(particles, numParticlesAlive);
            }
        }
        particlesDrawn = true;
    }

    void Destroy()
    {
        particleController.Clear();
        particlesDrawn = false;
    }
}
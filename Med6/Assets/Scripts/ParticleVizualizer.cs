using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleVizualizer : MonoBehaviour
{
    CSVReader CSVData;
    [Range(0.0f, 1.0f)]
    public float Min; 
    [Range(0.0f, 1.0f)]
    public float Max;
    float currentMin;
    public float currentMax;
    private ParticleSystem particleController;
    List<Vector3> XYZValues = new List<Vector3>();
    List<float> timeValues = new List<float>(); //Liste til alle tid værdier
    private ParticleSystem.Particle[] particles;
    private bool particlesDrawn = false;

    // Start is called before the first frame update
    void Start()
    {
        CSVData = GameObject.Find("CSV Reader").GetComponent<CSVReader>();
        timeValues = CSVData.timeVals();
        XYZValues = CSVData.XYZvals();
        Min = CSVData.Minval();
        Max = CSVData.Maxval();

        particleController = this.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[XYZValues.Count];
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
        Debug.Log(XYZValues[0]);
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.startSize = 1f;
        // Emit all the particles at once
        for (int i = 0; i < XYZValues.Count; i++)
        {
            if (timeValues[i] > Min)
            {
                int minCount = timeValues.Count(x => x > Min && x < Max);
                Debug.Log(" Min < " + minCount);
                var main = particleController.main;
                main.maxParticles = minCount;
                particleController.Emit(XYZValues.Count);
                int numParticlesAlive = particleController.GetParticles(particles);
                for (int j = 0; j < numParticlesAlive; j++)
                {
                        particles[j].position = XYZValues[j];
                }
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
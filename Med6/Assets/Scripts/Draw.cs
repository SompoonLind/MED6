using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public float speed = 0.2f; // Speed at which the color changes
    public Color startColor; // Starting color of the draw
    public Color endColor; // Ending color of the draw

    // Variables for creating a gradient to apply to the draw
    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;
    private Shader paintShader; // Shader used to draw
    private RenderTexture splatMap; // RenderTexture used for the draw
    private Material drawMaterial; // Material used for the draw

    // Variables for handling mouse input and timing the draw
    private RaycastHit hit;
    private float lastMouseMoveTime;
    private Vector3 lastMousePosition;
    private Vector3 currentHit;
    private Vector3 previousHit;
    public float timerInit = 0.01f;
    private float timeLeft;

    private void Start()
    {
        timeLeft = timerInit;
        lastMouseMoveTime = Time.time;
        lastMousePosition = Input.mousePosition;
        drawMaterial = new Material(Shader.Find("Unlit/Draw")); // Find the "Draw" shader and create a new material with it
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        GetComponent<Renderer>().material.mainTexture = splatMap;
    }


    private void Update()
    {
        Ray middleRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        timeLeft -= Time.deltaTime;

        // Calculate draw size based on the size of the object being hovered over
        float drawSize = 0.1f; // Default draw size
        if (Physics.Raycast(middleRay, out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null && renderer.material == GetComponent<Renderer>().material)
            {
                drawSize = Mathf.Max(renderer.bounds.size.x, renderer.bounds.size.y, renderer.bounds.size.z) / 10.0f; // Set the draw size based on the size of the object
            }
        }

        // Set up the gradient for the draw
        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = startColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = endColor;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        gradient.SetKeys(colorKey, alphaKey);

        currentHit = hit.point;

        // Handle mouse input and drawing
        if (currentHit != previousHit)
        {
            if (Physics.Raycast(middleRay, out hit))
            {
                if (hit.collider.GetComponent<Renderer>().material == GetComponent<Renderer>().material)
                {
                    // Set the draw coordinates and size based on the position of the mouse
                    drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                    drawMaterial.SetFloat("_DrawSize", drawSize);

                    // Apply the draw to the RenderTexture
                    RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    // Blit the splat map onto a temporary render texture
                    Graphics.Blit(splatMap, temp);
                    // Blit the temporary render texture onto the splat map using the draw material
                    Graphics.Blit(temp, splatMap, drawMaterial);
                    // Release the temporary render texture
                    RenderTexture.ReleaseTemporary(temp);
                }
            }
            // Use the draw material to set the color for the current brush stroke
            drawMaterial.SetVector("_Color", gradient.Evaluate(0f));
            previousHit = currentHit;
        }
        else
        {
            // Cast a ray from the camera to the mouse position and check for a collision
            if (Physics.Raycast(middleRay, out hit))
            {
                // Check if the collider of the hit object has the same material as this object
                if (hit.collider.GetComponent<Renderer>().material == GetComponent<Renderer>().material) // use default material
                {
                    // Use the draw material to set the coordinate for the current brush stroke
                    drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                    // Use the draw material to set the size for the current brush stroke
                    drawMaterial.SetFloat("_DrawSize", drawSize);
                    // Get a temporary render texture to store the splat map
                    RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    // Blit the splat map onto the temporary render texture
                    Graphics.Blit(splatMap, temp);
                    // Blit the temporary render texture onto the splat map using the draw material
                    Graphics.Blit(temp, splatMap, drawMaterial);
                    // Release the temporary render texture
                    RenderTexture.ReleaseTemporary(temp);
                    // Use the draw material to set the color for the current brush stroke
                    drawMaterial.SetVector("_Color", gradient.Evaluate(speed));
                }
            }
            // Reset the timer
            timeLeft = timerInit;
        }
    }
}
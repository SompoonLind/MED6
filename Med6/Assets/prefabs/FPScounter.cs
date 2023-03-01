 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
 
 public class FPScounter : MonoBehaviour {

     public float deltaTime;
     private int[] frames;
 
     void Update () {
         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         int CurrentFrame = (int)Mathf.Ceil(fps);
         foreach (var i in frames)
         {
            frames[i] = CurrentFrame;
            Debug.Log("Max FPS: " + Mathf.Max(frames[i]) + " | Min FPS: " + Mathf.Min(frames[i]));
         }
     }
 }
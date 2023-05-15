using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VisualController))]
public class visualizerEditor : Editor {
    public override void OnInspectorGUI()
    {
        var myScript = (VisualController)target;

        // Check which toggle is selected
        bool writeDataSelected = myScript.writeData;
        bool particlesSelected = myScript.particles;
        bool primitivesSelected = myScript.primitives;
        bool followAlongSelected = myScript.followAlong;

        // Draw the toggle buttons
        EditorGUILayout.BeginVertical(GUI.skin.box);

        using (var scope = new EditorGUI.DisabledScope(particlesSelected || primitivesSelected || followAlongSelected))
        {
            myScript.writeData = EditorGUILayout.Toggle("Write Data", writeDataSelected);
        }

        using (var scope = new EditorGUI.DisabledScope(writeDataSelected || primitivesSelected || followAlongSelected))
        {
            myScript.particles = EditorGUILayout.Toggle("Particles", particlesSelected);
        }

        using (var scope = new EditorGUI.DisabledScope(writeDataSelected || particlesSelected || followAlongSelected))
        {
            myScript.primitives = EditorGUILayout.Toggle("Primitives", primitivesSelected);
        }

        using (var scope = new EditorGUI.DisabledScope(writeDataSelected || particlesSelected || primitivesSelected))
        {
            myScript.followAlong = EditorGUILayout.Toggle("Follow Along", followAlongSelected);
        }

        EditorGUILayout.EndVertical();
    }
}
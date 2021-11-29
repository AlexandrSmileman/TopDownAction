using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var component = (MazeGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            component.Generate();
        }
    }
}

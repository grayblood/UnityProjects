using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MapGeneratorEditor : Editor
{
  public override void OnInspectorGUI()
    {
        MeshGenerator mapGen = (MeshGenerator)target;
        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData obstacleData = (ObstacleData)target;

        EditorGUI.BeginChangeCheck();

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                obstacleData.obstacleGrid[x, y] = EditorGUILayout.Toggle(obstacleData.obstacleGrid[x, y]);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(obstacleData);
        }
    }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavigationSystemSettings))]
public class NavigationSystemEditor : Editor
{
    private SerializedProperty pointsProperty;
    private bool isEditMode = false;

    private void OnEnable()
    {
        pointsProperty = serializedObject.FindProperty("points");
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();
        
        if (GUILayout.Button(isEditMode ? "Выход из режима редактирования" : "Режим редактирования"))
        {
            isEditMode = !isEditMode;
        }

        serializedObject.ApplyModifiedProperties();
    }
    

    public void OnSceneGUI(SceneView scene)
    {
        if (!isEditMode)
            return;
        NavigationSystemSettings data = (NavigationSystemSettings)target;
        
        Vector3 point1 = Handles.PositionHandle(data.points[0], Quaternion.identity);

        if (data == null || data.points == null)
            return;

        for (int i = 0; i < data.points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 point = Handles.PositionHandle(data.points[i], Quaternion.identity);
            Handles.DrawLine(data.points[i], data.points[data.indexPointsTo[i]]);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(data, "Move Point");
                data.points[i] = point;
                EditorUtility.SetDirty(data);
            }
        }
    }
}

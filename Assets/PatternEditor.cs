using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pattern))]
public class PatternEditor : Editor
{
    Pattern pattern;

    void OnEnable()
    {
        pattern = target as Pattern;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Node Pattern");
        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                pattern.inspectorShowPattern[i].index[j] = (NodeType)EditorGUILayout.EnumPopup(pattern.inspectorShowPattern[i].index[j]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

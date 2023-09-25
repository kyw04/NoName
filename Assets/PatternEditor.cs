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

        //EditorGUILayout.LabelField("Pattern");
        //for (int i = 0; i < 3; i++)
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    for (int j = 0; j < 3; j++)
        //    {
        //        pattern.inspectorShowPattern[i].index[j] = (NodeBase)EditorGUILayout.ObjectField(pattern.inspectorShowPattern[i].index[j], typeof(NodeBase), true);
        //    }
        //    EditorGUILayout.EndHorizontal();
        //}

        //EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Pattern Color");
        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                NodeBase nodeBase = pattern.inspectorShowPattern[i].index[j];
                Color color = Color.gray;
                string nodeName = "None";

                if (nodeBase)
                {
                    color = nodeBase.color;
                    nodeName = nodeBase.name;
                }
                GUI.color = color;
                GUILayout.Button("");
            }
            EditorGUILayout.EndHorizontal();
        }
        GUI.color = Color.white;
        serializedObject.Update();
    }
}

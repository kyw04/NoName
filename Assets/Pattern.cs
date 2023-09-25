using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodePattern
{
    public NodeType[] index = new NodeType[3];
}

[CreateAssetMenu(fileName = "NodePattern", menuName = "Tools/NodePattern")]
public class Pattern : ScriptableObject
{
    [HideInInspector]
    public NodePattern[] inspectorShowPattern = new NodePattern[3];
    public NodeType[,] nodePattern = new NodeType[3, 3];

    public int damage;
}

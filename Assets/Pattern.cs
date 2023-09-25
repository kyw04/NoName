using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodePattern
{
    public NodeBase[] index = new NodeBase[3];
}

[CreateAssetMenu(fileName = "NodePattern", menuName = "Tools/NodePattern")]
public class Pattern : ScriptableObject
{
    public NodePattern[] inspectorShowPattern = new NodePattern[3];
    public NodeType[,] nodePattern = new NodeType[3, 3];

    public int damage;
}

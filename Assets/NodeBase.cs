using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    None,
    Red,
    Blue,
    White,
    Green,
    Black
}

[CreateAssetMenu(fileName = "NodeBase", menuName = "Tools/NodeBase")]
public class NodeBase : ScriptableObject
{
    public enum State
    {
        Idle,
        Hold
    }

    public NodeType type;
    public State state;
    public Color32 color;
}

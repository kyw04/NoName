using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeBase", menuName = "Tools/NodeBase")]
public class NodeBase : ScriptableObject
{
    public enum Type
    {
        None,
        Red,
        Blue,
        White,
        Green,
        Black
    }
    public enum State
    {
        Idle,
        Hold
    }

    public Type type;
    public State state;
    public Color32 color;
}

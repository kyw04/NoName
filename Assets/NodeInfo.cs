using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeInfo", menuName = "Tools/NodeInfo")]
public class NodeInfo : ScriptableObject
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

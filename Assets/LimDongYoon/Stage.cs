using UnityEngine;
using System;

public enum LineType { Battle, Event, Elite }

public class Stage
{
    public LineType lineType;
    public Vector2 position; // Position of the stage on the map

    public Stage(LineType lineType, Vector2 position)
    {
        this.lineType = lineType;
        this.position = position;
    }
    // Other data...
}
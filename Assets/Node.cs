using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
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

    public int x, y;
    public RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Change(Vector2 movePos)
    {

    }

    public void Hold()
    {
        Debug.Log($"hold {x}, {y}");
    }

    public void Put()
    {
        Debug.Log($"put {x}, {y}");
    }
}

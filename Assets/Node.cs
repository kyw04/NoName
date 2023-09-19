using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private Image image;

    public NodeBase nodeBase;
    public float maxDistance;
    private float moveDistance;
    private Transform parent;
    private int x, y;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = nodeBase.color;
    }

    public void Change(Node node)
    {
        Node temp = new Node();
        temp.Set(x, y, parent);

        this.Set(node.x, node.y, node.parent);
        node.Set(temp.x, temp.y, temp.parent);
    }
    public void Set(int _x, int _y, Transform _parent)
    {
        x = _x;
        y = _y;
        parent = _parent;
    }

    public void Hold()
    {
        Vector3 moveDirection = Input.mousePosition - parent.position;
        moveDistance = Vector3.Distance(Input.mousePosition, parent.position);
        moveDistance = maxDistance < moveDistance ? maxDistance : moveDistance;
        transform.position = moveDirection.normalized * moveDistance + parent.position;
    }

    public void Put()
    {
        if (moveDistance > maxDistance * 0.5f)
        {
            Debug.Log("change");
        }
        transform.position = parent.position;
    }
}

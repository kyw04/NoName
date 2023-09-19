using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public Transform[] pos;
    public GameObject nodePrefab;
    public Transform nodeParent;
    public List<Node> nodes = new List<Node>();

    private void Start()
    {
        for (int i = 0, j = 0; i < pos.Length; i++)
        {
            Node newNode = Instantiate(nodePrefab, pos[i]).GetComponent<Node>();
            newNode.x = i % 5;
            newNode.y = j % 5;
            j = i % 5 == 0 ? j : j + 1;

            nodes.Add(newNode);
        }
    }

    public void NodeDelete(int x, int y)
    {

    }

    public void NodeDown(int x, int y, int cnt)
    {
        for (int i = y - cnt; i >= 0; i--)
        {
            //nodes[x, i + 1] = nodes[x, i];
            Debug.Log($"{i + 1} <= {i}");
        }
        //nodes[x, 0] = change;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 i:  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14
 x:  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  2,  2,  2,  2,  2
 y:  0,  1,  2,  3,  4,  0,  1,  2,  3,  4,  0,  1,  2,  3,  4
*/

public class Puzzle : MonoBehaviour
{
    public Transform[] pos;
    public GameObject nodePrefab;
    public Transform nodeParent;
    public NodeBase[] nodeInfos;
    public List<Node> nodes = new List<Node>();

    private void Start()
    {
        for (int i = 0, j = 0; i < pos.Length; i++)
        {
            Node newNode = Instantiate(nodePrefab, pos[i]).GetComponent<Node>();
            newNode.puzzle = this;
            newNode.nodeBase = nodeInfos[Random.Range(0, nodeInfos.Length)];
            newNode.Set(i % 5, j % 5);
            j = (i + 1) % 5 == 0 ? j + 1 : j;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 i:  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14
 x:  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  2,  2,  2,  2,  2
 y:  0,  1,  2,  3,  4,  0,  1,  2,  3,  4,  0,  1,  2,  3,  4
*/

public class Puzzle : MonoBehaviour
{
    public GameObject[] countImg;
    public Transform[] pos;
    public GameObject nodePrefab;
    public NodeBase[] nodeInfos;
    public List<Node> nodes = new List<Node>();
    public int changeCount = 3;
    public Pattern[] patterns;

    private void Awake()
    {
        // no changed in Inspector. i'll handle it here.
        foreach (Pattern pattern in patterns)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pattern.nodePattern[i, j] = pattern.inspectorShowPattern[i].index[j];
                }
            }
        }
    }

    private void Start()
    {
        foreach (GameObject img in countImg)
        {
            img.SetActive(true);
        }

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

    public void UseCount()
    {
        if (changeCount <= 0)
            return;

        changeCount--;
        countImg[changeCount].SetActive(false);
    }

    public void TurnEnd()
    {
        foreach (Pattern pattern in patterns)
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (PatternCheck(pattern, i, j))
                        Debug.Log("true");
                }
            }
        }
    }

    public bool PatternCheck(Pattern pattern, int x, int y)
    {
        int[] dir = { -1, 0, 1 };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (pattern.nodePattern[i, j] == NodeType.None)
                    continue;

                int newX = x + dir[i];
                int newY = y + dir[j];
                int index = newX * 5 + newY;
                if (pattern.nodePattern[i, j] != nodes[index].nodeBase.type)
                    return false;
            }
        }

        return true;
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

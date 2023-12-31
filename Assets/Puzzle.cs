using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 i:  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14
 x:  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  2,  2,  2,  2,  2
 y:  0,  1,  2,  3,  4,  0,  1,  2,  3,  4,  0,  1,  2,  3,  4
*/

public class Puzzle : MonoBehaviour
{
    public enum PuzzleState
    { 
        Idle,
        Attack,
        Waiting,
        BoardModifi
    }


    public GameObject[] countImg;
    public Transform[] pos;
    public Node nodeDefault;
    public NodeBase[] nodeInfos;
    public List<Node> nodes = new List<Node>();
    public int changeCount = 3;
    public Pattern[] patterns;
    public Enemy enemy;
    public Player player;

    private int deleteCount;
    public PuzzleState puzzleState;

    private void Awake()
    {
        // no changed in Inspector. i'll handle it here.
        foreach (Pattern pattern in patterns)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pattern.inspectorShowPattern[i].index[j])
                        pattern.nodePattern[i, j] = pattern.inspectorShowPattern[i].index[j].type;
                    else
                        pattern.nodePattern[i, j] = NodeType.None;
                }
            }
        }
    }

    private void Start()
    {
        ResetCount();

        for (int i = 0, j = 0; i < pos.Length; i++)
        {
            Node newNode = Instantiate(nodeDefault, pos[i]);
            newNode.puzzle = this;
            newNode.nodeBase = nodeInfos[Random.Range(0, nodeInfos.Length)];
            newNode.SetIndex(i % 5, j % 5);
            j = (i + 1) % 5 == 0 ? j + 1 : j;
            nodes.Add(newNode);
        }

        bool[] selected = new bool[nodeInfos.Length];
        for (int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, nodeInfos.Length);
            while (selected[index]) { index = Random.Range(0, nodeInfos.Length); }
            selected[index] = true;

            enemy.SetDeleteNodes(i, nodeInfos[index], Random.Range(1, 3));
        }
        enemy.puzzle = this;
    }

    private void Update()
    {
        if (deleteCount == 0)
        {
            deleteCount = -1;

            if (puzzleState == PuzzleState.BoardModifi)
            {
                puzzleState = PuzzleState.Attack;
                NodeDelete(enemy.Attack());
            }
            else
            {
                puzzleState = PuzzleState.Idle;
            }
        }
    }

    private void ResetCount()
    {
        changeCount = 3;
        foreach (GameObject img in countImg)
        {
            img.SetActive(true);
        }
    }

    public void UseCount()
    {
        if (changeCount <= 0)
            return;

        changeCount--;
        countImg[changeCount].SetActive(false);

        if (changeCount == 0)
            puzzleState = PuzzleState.Waiting;
    }

    public void TurnEnd()
    {
        if (puzzleState != PuzzleState.Idle && puzzleState != PuzzleState.Waiting)
            return;

        puzzleState = PuzzleState.BoardModifi;

        HashSet<Node> deleteNodes = new HashSet<Node>();

        foreach (Pattern pattern in patterns)
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    HashSet<Node> temp = PatternCheck(pattern, i, j);
                    if (temp.Count() > 0)
                    {
                        deleteNodes.UnionWith(temp);
                    }
                }
            }
        }

        NodeDelete(deleteNodes);
        ResetCount();
    }

    public HashSet<Node> PatternCheck(Pattern pattern, int x, int y)
    {
        int[] dir = { -1, 0, 1 };
        var deleteNode = new HashSet<Node>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (pattern.nodePattern[i, j] == NodeType.None)
                    continue;

                int newX = x + dir[i];
                int newY = y + dir[j];
                int index = newX * 5 + newY;
                if (pattern.nodePattern[i, j] == nodes[index].nodeBase.type)
                {
                    deleteNode.Add(nodes[index]);
                }
                else
                {
                    deleteNode.Clear();
                    return deleteNode;
                }
            }
        }

        enemy.GetDamage(pattern.damage);
        return deleteNode;
    }

    public void NodeDelete(HashSet<Node> deleteNode)
    {
        HashSet<int> index = new HashSet<int>();
        foreach (Node node in deleteNode)
        {
            index.Add(node.x);
            node.SetNode(nodeDefault);
        }

        deleteCount = index.Count;
        foreach (int i in index)
        {
            StartCoroutine(NodeDown(i, 4));
        }
    }

    public IEnumerator NodeDown(int x, int y)
    {
        while (y >= 0)
        {
            if (nodes[x + y * 5].nodeBase.type == NodeType.None)
            {
                int i = y;
                for (; i >= 0; i--)
                {
                    if (nodes[x + i * 5].nodeBase.type != NodeType.None)
                    {
                        yield return new WaitForSeconds(0.75f);
                        nodes[x + i * 5].Change(nodes[x + y * 5]);
                        break;
                    }
                }

                if (i == -1)
                {
                    yield return new WaitForSeconds(0.75f);
                    nodes[x + y * 5].SetNode(nodeInfos[Random.Range(0, nodeInfos.Length)]);
                }
            }
            y--;
        }

        yield return new WaitForSeconds(0.75f);
        deleteCount--;
    }
}

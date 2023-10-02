using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public struct DeleteNode
    {
        public NodeBase nodeBase;
        public int count;
    };

    public Puzzle puzzle;

    [Space(10)]
    public GameObject deleteNodeInfoPrefab;
    public Transform deleteNodeParent;
    public DeleteNode[] deleteNodes;
    public Image healthBar;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damage;

    private void Start()
    {
        deleteNodes = new DeleteNode[2];
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void SetDeleteNodes(int index, NodeBase nodeBase, int count)
    {
        deleteNodes[index].nodeBase = nodeBase;
        deleteNodes[index].count = count;

        GameObject newInfo = Instantiate(deleteNodeInfoPrefab, deleteNodeParent);
        newInfo.GetComponentInChildren<Image>().color = nodeBase.color;
        newInfo.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    public HashSet<Node> Attack()
    {
        HashSet<Node> result = new HashSet<Node>();

        foreach (DeleteNode deleteNode in deleteNodes)
        {
            List<Node> sameNode = new List<Node>();

            foreach (Node node in puzzle.nodes)
            {
                if (node.nodeBase == deleteNode.nodeBase)
                {
                    sameNode.Add(node);
                }
            }

            for (int i = 0; i < deleteNode.count; i++)
            {
                if (sameNode.Count == 0)
                    break;

                int index = Random.Range(0, sameNode.Count);
                result.Add(sameNode[index]);
                sameNode.RemoveAt(index);
                puzzle.player.GetDamage(damage);
            }
        }

        return result;
    }
}

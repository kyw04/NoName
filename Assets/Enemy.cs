using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public struct DeleteNodes
    {
        public NodeBase nodeBase;
        public int count;
    };

    public DeleteNodes[] deleteNodes;
    public Image healthBar;
    public int maxHealth = 100;
    public int currentHealth = 100;

    private void Start()
    {
        deleteNodes = new DeleteNodes[2];
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
    }
}

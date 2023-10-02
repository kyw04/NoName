using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image healthBar;
    public int maxHealth;
    public int currentHealth;

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}

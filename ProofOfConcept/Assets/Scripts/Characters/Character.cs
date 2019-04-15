using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    internal int health;
    internal readonly int maxHealth = 5;
    bool ded;
    public Character(int healthMax=5)
    {
        maxHealth = healthMax;
        health = maxHealth;
    }
    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }

    }
    internal virtual void Die()
    {
        GameManager.gameManager.enemies.Remove(gameObject);
        Destroy(gameObject, 0.5f);
    }
}
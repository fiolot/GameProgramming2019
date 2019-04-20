using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public abstract class Character : MonoBehaviour
{
    [HideInInspector]public readonly int maxHealth;
    [HideInInspector]public int health;
    public Character(int maxHealth = 5)
    {
        maxHealth = this.maxHealth;
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {

    }
}
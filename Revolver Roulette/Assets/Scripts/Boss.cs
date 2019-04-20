using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float movementSpeed = 1.0f;
    public Boss(int maxHealth = 5) : base(maxHealth)
    {

    }
}
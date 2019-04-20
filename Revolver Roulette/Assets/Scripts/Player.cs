using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    public class Player : Character
    {
        [SerializeField] float movementSpeed = 1.0f;
        public Player(int maxHealth = 5) : base(maxHealth)
        {

        }
    }
}
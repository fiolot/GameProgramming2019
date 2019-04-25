using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    public class Player : Character
    {
        public Player(int maxHealth = 5) : base(maxHealth)
        {

        }
        private void Reset()
        {
            maxHealth = 5;
            health = maxHealth;
        }
    }
}
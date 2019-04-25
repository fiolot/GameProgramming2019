using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    [RequireComponent(typeof(Collider))]
    public class Character : MonoBehaviour
    {
        public int maxHealth;
        public int health;
        internal Character(int maxHealth = 5)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            Debug.Log(health + "/" + maxHealth);
            HealthCheck();
        }
        private void HealthCheck()
        {
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
        virtual protected void Die()
        {
            Debug.Log("Make code for Die()!");
        }
    }
}
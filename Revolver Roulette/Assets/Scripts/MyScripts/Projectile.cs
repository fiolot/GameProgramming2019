using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        int damage = 1;
        [SerializeField]
        float speed;
        [SerializeField]
        float timeToDeath;
        Rigidbody projectile;
        private void Start()
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
            Destroy(gameObject, timeToDeath);
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Character")
            {
                other.gameObject.GetComponent<Character>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
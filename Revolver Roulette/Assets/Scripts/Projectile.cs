using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;
    Rigidbody projectile;
    private void OnCollisionEnter(Collision other)
    {
        
    }
}
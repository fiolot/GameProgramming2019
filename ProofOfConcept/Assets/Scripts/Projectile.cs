using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public int damage = 1, speed = 5;
    public float secondsToDie = 1.0f;
    private void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * speed * 1000);
        Destroy(gameObject, secondsToDie);

    }
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
        catch
        {

        }
        Destroy(gameObject);
    }
}
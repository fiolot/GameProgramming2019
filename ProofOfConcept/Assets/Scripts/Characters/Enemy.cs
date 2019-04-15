using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject projectile;
    GameObject player;
    Vector3 direction;
    bool canFire = true;
    int index;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Scheise");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Got player.");
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }
    private void Update()
    {
        if(player != null)
        {
            Debug.Log("Have Player");
            direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            if (canFire)
                StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        canFire = false;
        Instantiate(projectile, transform.position + direction * 5, transform.rotation * Quaternion.Euler(90, 0, 0)).GetComponent<Projectile>().speedMult = 1000;
        yield return new WaitForSeconds(1.0f);
        index++;
        if (index > 5)
        {
            Debug.Log("Reloading!");
            yield return new WaitForSeconds(2.0f);
            index = 0;
        }
        canFire = true;
    }
}
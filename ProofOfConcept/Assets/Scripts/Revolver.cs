﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Revolver : MonoBehaviour
{
    public GameObject projectile;
    public GameObject ammoCylinder;
    public Renderer[] ammo = new Renderer[6];
    private bool[] chambers = new bool[6];
    int index = 0;
    bool canFire;
    public Text bulletsText;
    int bullets, maxBullets = 2;
    private void Start()
    {
        StartCoroutine("Reload");
    }
    private void Update()
    {
        bulletsText.text = "Bullets: " + bullets + "/" + maxBullets;
        index = index >= chambers.Length ? 0 : index;
        if (canFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(TryFire());
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Skip a chamber.");
                SkipChamber();
            }
        }
    }
    IEnumerator TryFire()
    {
        canFire = false;
        if (chambers[index])
        {
            Fire();
            yield return new WaitForSeconds(0.1f);
            if (EmptyChambers())
            {
                StartCoroutine("Reload");
                Debug.Log("Reloading");
                canFire = false;
                yield break;
            }
            canFire = true;
        }
        else
        {
            Debug.Log("Blank!");
            yield return new WaitForSeconds(0.4f);
            canFire = true;
        }
        SkipChamber();
    }
    void Fire()
    {
        //Make Object Pooling!
        Debug.Log("Try to shoot!");
        chambers[index] = false;
        ammo[index].enabled = false;
        Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>().damage = 5;
        bullets--;
    }
    bool EmptyChambers()
    {
        for (int i = 0; i < chambers.Length; i++)
        {
            if (chambers[i])
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator Reload()
    {
        int firstRandom = Random.Range(0, 6);
        int secondRandom;
        yield return new WaitForSecondsRealtime(1.0f);
        index = 0;
        ammoCylinder.transform.localRotation = Quaternion.Euler(0, 0, 0);
        chambers[firstRandom] = true;
        do
        {
            secondRandom = Random.Range(0, 6);
        } while (secondRandom == firstRandom);
        chambers[secondRandom] = true;
        ammoCylinder.transform.localPosition = new Vector3(-1.2f, ammoCylinder.transform.localPosition.y, ammoCylinder.transform.localPosition.z);
        ammo[firstRandom].enabled = true;
        ammo[secondRandom].enabled = true;
        yield return new WaitForSeconds(0.2f);
        ammoCylinder.transform.localPosition = new Vector3(0, ammoCylinder.transform.localPosition.y, ammoCylinder.transform.localPosition.z);
        bullets = 2;
        canFire = true;
        yield break;
    }
    void SkipChamber()
    {
        index++;
        ammoCylinder.transform.Rotate(0, 60, 0, Space.Self);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    public class Enemy : Character
    {
        [SerializeField]
        LayerMask layer;
        [SerializeField]
        GameObject projectilePrefab;
        [SerializeField]
        Transform spawnProjectileTransform;
        GameController gameController;
        Transform pTransform;
        Vector3 heading;
        int bullets;
        bool canFire = true, reloading;
        public Enemy(int maxHealth = 5) : base(maxHealth)
        {

        }
        private void Reset()
        {
            maxHealth = 5;
            health = maxHealth;
        }
        private void Start()
        {
            reloading = false;
            bullets = 6;
            canFire = true;
            gameController = GameController.staticGameController;
            gameController.enemies.Add(this);
            pTransform = gameController.pController.transform;
        }
        internal void MyUpdate()
        {
            Debug.Log(SeePlayer());
            if (bullets == 0 && !reloading)
            {
                Debug.Log("You shouldn't be firing bitch");
                canFire = false;
                StartCoroutine(Reload());
            }
            else if (SeePlayer() && canFire && !reloading)
            {
                canFire = false;
                transform.rotation = Quaternion.LookRotation(heading);
                StartCoroutine(Fire());
            }
        }
        bool SeePlayer()
        {
            heading = pTransform.position - transform.position;
            return !Physics.Raycast(transform.position, heading, heading.magnitude, layer);
        }
        public IEnumerator Fire()
        {
            Debug.Log("Fired a projectile!");
            Instantiate(projectilePrefab, spawnProjectileTransform.position, transform.rotation);
            bullets--;
            yield return new WaitForSeconds(12.0f);
            canFire = true;
        }
        IEnumerator Reload()
        {
            reloading = true;
            yield return new WaitForSecondsRealtime(6.0f);
            canFire = true;
            reloading = false;
            bullets = 6;
        }
        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}
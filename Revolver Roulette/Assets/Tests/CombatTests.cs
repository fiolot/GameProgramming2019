using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RevolverRoulette;
namespace Tests
{
    public class CombatTests
    {
        public GameObject projectile;
        public GameObject player;
        public GameObject enemy;
        public GameObject instanciatedProjectile, instanciatedPlayer, instanciatedEnemy, instanciatedRevolver;
        private Vector3 playerPos = new Vector3(0, 0, -90), enemyPos = new Vector3(50, 0, 10);
        private Revolver revolver;

        [SetUp]
        public void Setup()
        {
            projectile = Resources.Load<GameObject>("Projectile");
            player = Resources.Load<GameObject>("Player");
            enemy = Resources.Load<GameObject>("Enemy");
            instanciatedPlayer = GameObject.Instantiate(player);
            instanciatedPlayer.transform.position = playerPos;
            revolver = instanciatedPlayer.GetComponentInChildren<Revolver>();
            instanciatedEnemy = GameObject.Instantiate(enemy);
            instanciatedEnemy.transform.position = enemyPos;
            Time.timeScale = 20;
        }
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(instanciatedProjectile);
            GameObject.Destroy(instanciatedPlayer);
            GameObject.Destroy(instanciatedEnemy);
            Time.timeScale = 1;
        }

        [UnityTest]
        public IEnumerator RevovlerCanFire()
        {
            revolver.projectileSpawnTransform.position = new Vector3(enemyPos.x, 0, 0);
            revolver.GetComponent<Revolver>().Fire();
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(instanciatedEnemy.GetComponent<Enemy>().maxHealth, instanciatedEnemy.GetComponent<Enemy>().health);
        }
        [UnityTest]
        public IEnumerator EnemyCanFire()
        {
            instanciatedPlayer.transform.position = new Vector3(enemyPos.x, 0, 5);
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(instanciatedPlayer.GetComponent<Player>().maxHealth, instanciatedPlayer.GetComponent<Player>().health);
        }

        [UnityTest]
        public IEnumerator ProjectileCanMove()
        {
            instanciatedProjectile = GameObject.Instantiate(projectile, new Vector3(0, 0, 0), revolver.projectileSpawnTransform.rotation);
            Vector3 posZero = instanciatedProjectile.transform.position;
            yield return new WaitForSeconds(0.3f);
            Vector3 posLater = instanciatedProjectile.transform.position;
            Assert.AreNotEqual(posZero, posLater);
        }
        [UnityTest]
        public IEnumerator ProjectileCanDamageEnemy()
        {
            instanciatedProjectile = GameObject.Instantiate(projectile, new Vector3(enemyPos.x, 0, 0), revolver.projectileSpawnTransform.rotation);
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(instanciatedEnemy.GetComponent<Enemy>().maxHealth, instanciatedEnemy.GetComponent<Enemy>().health);
        }
        [UnityTest]
        public IEnumerator ProjectileCanDamagePlayer()
        {
            instanciatedPlayer.transform.position = new Vector3(playerPos.x, 0, 10);
            instanciatedProjectile = GameObject.Instantiate(projectile, new Vector3(playerPos.x, 0, -10), revolver.projectileSpawnTransform.rotation);
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(instanciatedPlayer.GetComponent<Player>().maxHealth, instanciatedPlayer.GetComponent<Player>().health);
        }

        [UnityTest]
        public IEnumerator AreThereBulletsInTheChambers()
        {
            int countingBullets = 0;
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                revolver.index = i;
                if (revolver.CheckCurrentChamber())
                {
                    countingBullets++;
                }
            }
            Assert.AreEqual(2, countingBullets);
            yield break;
        }
        [UnityTest]
        public IEnumerator RevolverCanReload()
        {
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                revolver.chambers[i] = false;
            }
            revolver.Reload();
            int countingBullets = 0;
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                if (revolver.chambers[i])
                {
                    countingBullets++;
                }
            }
            Assert.AreEqual(2, countingBullets);
            yield break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolverRoulette
{
    public class GameController : MonoBehaviour
    {
        internal static GameController staticGameController;
        internal PlayerController pController;
        internal List<Enemy> enemies = new List<Enemy>();
        private void Awake()
        {
            if (staticGameController == null)
            {
                staticGameController = this;
            }
            else
            {
                Destroy(this);
            }
        }
        private void Update()
        {
            if (enemies.Count == 0)
            {
                //Win state
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.MyUpdate();
            }
        }
    }
}
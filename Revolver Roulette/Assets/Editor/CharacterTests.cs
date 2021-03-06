﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RevolverRoulette;
namespace Tests
{
    public class CharacterTests
    {
        private Player player;
        private Enemy enemy;
        private const int damage = 1;
        private const int health = 3;

        [SetUp]
        public void SetupTest()
        {
            player = new Player(health);
            enemy = new Enemy(health);
        }
        [TearDown]
        public void TearDownTest()
        {
            player = null;
            enemy = null;
        }
        [Test]
        public void PlayerTakeDamage()
        {
            player.TakeDamage(damage);
            Assert.AreNotEqual(player.maxHealth, player.health);
        }
        [Test]
        public void EnemyTakeDamage()
        {
            enemy.TakeDamage(damage);
            Assert.AreNotEqual(enemy.maxHealth, enemy.health);
        }
        [Test]
        public void PlayerStartWithMaxHealth()
        {
            player = new Player();
            Assert.AreEqual(player.maxHealth, player.health);
        }
        [Test]
        public void EnemyStartWithMaxHealth()
        {
            enemy = new Enemy();
            Assert.AreEqual(enemy.maxHealth, enemy.health);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RevolverRoulette;

namespace Tests
{
    public class RevovlerTestsEditor
    {
        private Revolver revolver;
        [SetUp]
        public void SetupTest()
        {
            revolver = new Revolver();
        }
        [TearDown]
        public void TearDownTest()
        {
            revolver = null;
        }
        [Test]
        public void AreThereBulletsInTheChambers()
        {
            revolver.Reload();
            int countingBullets = 0;
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                revolver.index = i;
                countingBullets = revolver.CheckChamber() ? countingBullets++ : countingBullets;
            }
            Assert.AreEqual(countingBullets, 2);
        }
        [Test]
        public void CanSkipChamber()
        {
            revolver.SkipChamber();
            Assert.AreNotEqual(revolver.index, 0);
        }
        [Test]
        public void DoesReloadWork()
        {
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                revolver.chambers[i].enabled = false;
            }
            revolver.Reload();
            int countingBullets = 0;
            for (int i = 0; i < revolver.chambers.Length; i++)
            {
                countingBullets = revolver.chambers[i].enabled ? countingBullets++ : countingBullets;
            }
            Assert.AreEqual(countingBullets, 2);
        }
    }
}

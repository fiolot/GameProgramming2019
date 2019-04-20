using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolverRoulette
{
    public class Revolver : MonoBehaviour
    {
        public Renderer[] chambers = new Renderer[6];
        bool canFire = true;
        public int index = 0;
        public bool CheckChamber()
        {
            return false;
        }
        public void Fire()
        {

        }
        public void SkipChamber()
        {

        }
        public void Reload()
        {

        }
    }
}
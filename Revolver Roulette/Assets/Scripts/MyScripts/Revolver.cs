using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolverRoulette
{
    public class Revolver : MonoBehaviour
    {
        public Renderer[] visualChambers = new Renderer[6];
        [HideInInspector] public bool[] chambers = new bool[6];
        [HideInInspector] public int index = 0;
        [SerializeField] private GameObject projectilePrefab;
        public Transform projectileSpawnTransform;
        [SerializeField] private Transform ammoCylinder;
        bool canFire = true;
        private void Start()
        {

        }
        public bool EmptyChambers()
        {
            foreach (bool chamber in chambers)
            {
                if (chamber)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckCurrentChamber()
        {
            if (chambers[index])
            {
                return true;
            }
            return false;
        }
        public void Fire()
        {
            Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
            SetChamber(index, false);
            SkipChamber();
        }
        private void SetChamber(int index, bool value)
        {
            chambers[index] = value;
            visualChambers[index].enabled = value;
        }
        public void SkipChamber()
        {
            if (index >= chambers.Length - 1)
            {
                index = 0;
                ammoCylinder.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                index++;
                ammoCylinder.Rotate(0, 60, 0, Space.Self);
            }
        }
        public void Reload()
        {
            index = 0;
            int firstRandomNumber, secondRandomNumber;
            firstRandomNumber = Random.Range(0, chambers.Length);
            do
            {
                secondRandomNumber = Random.Range(0, chambers.Length);
            } while (secondRandomNumber == firstRandomNumber);
            SetChamber(firstRandomNumber, true);
            SetChamber(secondRandomNumber, true);
        }
        internal void TryFire()
        {
            if (canFire)
            {
                canFire = false;
                StartCoroutine(MouseButtonZero());
            }
        }
        internal IEnumerator MouseButtonZero()
        {
            if (!CheckCurrentChamber())
            {
                yield return new WaitForSecondsRealtime(1f);
                SkipChamber();
                canFire = true;
                yield break;
            }
            Fire();
            yield return new WaitForSecondsRealtime(0.4f);
            if (EmptyChambers())
            {
                StartCoroutine(VisualReload());
            }
            else
            {
                canFire = true;
            }
            yield break;
        }
        public IEnumerator VisualReload()
        {
            ammoCylinder.localRotation = Quaternion.Euler(0, 0, 0);
            ammoCylinder.localPosition = new Vector3(ammoCylinder.localPosition.x - 1.5f, ammoCylinder.localPosition.y, ammoCylinder.localPosition.z);
            yield return new WaitForSecondsRealtime(0.4f);
            Reload();
            yield return new WaitForSecondsRealtime(0.3f);
            ammoCylinder.localPosition = new Vector3(ammoCylinder.localPosition.x + 1.5f, ammoCylinder.localPosition.y, ammoCylinder.localPosition.z);
            canFire = true;
            yield break;
        }
    }
}
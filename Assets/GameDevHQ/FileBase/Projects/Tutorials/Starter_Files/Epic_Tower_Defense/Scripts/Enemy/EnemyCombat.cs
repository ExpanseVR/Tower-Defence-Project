using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts;
using GameDevHQ.Scripts.Managers;

namespace GameDevHQ.Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        Transform _toRotate;

        [SerializeField]
        private float _rotateSpeed;

        private List<Tower> _towers = new List<Tower>();

        private void OnTriggerEnter(Collider other)
        {
            //Detect tower
            if (other.gameObject.tag == "Gatling_Gun_Lvl1")
            {
                //Add tower to list
                _towers.Add(other.GetComponent<Tower>());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //if there is a tower in range
            if (_towers.Count > 0)
            {
                //rotate towards first tower in list
                Vector3 currentTarget = _towers[0].gameObject.transform.position;
                Vector3 targetDirection = (currentTarget - transform.position).normalized;
                _toRotate.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                //attack that tower
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Detect tower
            if (other.gameObject.tag == "Gatling_Gun_Lvl1")
            {
                //remove tower from list 
                _towers.Remove(other.GetComponent<Tower>());
            }
            if (_towers.Count < 1)
                StartCoroutine(LerpRotation(_toRotate.rotation, Quaternion.Euler(Vector3.forward)));
        }

        IEnumerator LerpRotation (Quaternion startRotation, Quaternion finalRotation)
        {
            float progress = 0f;

            while (progress < 1f)
            {
                _toRotate.rotation = Quaternion.Slerp(startRotation, finalRotation, progress);
                progress += Time.deltaTime * _rotateSpeed;
                yield return null;
            }
        }
    }
}

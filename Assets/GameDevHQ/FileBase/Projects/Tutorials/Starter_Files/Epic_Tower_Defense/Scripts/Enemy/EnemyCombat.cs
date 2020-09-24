using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts;

namespace GameDevHQ.Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        Transform _toRotate;

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
                targetDirection.y = 0;
                _toRotate.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                //attack that tower
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //remove tower from list   
        }
    }
}

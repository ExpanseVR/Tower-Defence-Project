using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class EnemyGoal : MonoBehaviour
    {
        //deactivate enemy when it reaches goal
        private void OnTriggerEnter(Collider other)
        {
            var isEnemy = other.gameObject.GetComponent<Enemy>(); 
            if (other.gameObject.tag != null)
                isEnemy.gameObject.SetActive(false);
        }
    }
}
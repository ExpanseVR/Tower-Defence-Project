using GameDevHQ.Scripts.Managers;
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
            {
                EventManager.Fire(EventManager.Events.EnemyGoalReached.ToString(), -isEnemy.GetPlayerDamage());
                isEnemy.gameObject.SetActive(false);
            }
        }
    }
}
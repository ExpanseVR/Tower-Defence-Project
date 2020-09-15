using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField]
        protected int warFundsCost;
        [SerializeField]
        protected int towerRange;

        protected List<Enemy> targets = new List<Enemy>();
        protected SphereCollider targetCollider;


        public virtual void Start()
        {
            targetCollider = transform.GetComponent<SphereCollider>();
            targetCollider.radius = towerRange;
        }

        protected void Update()
        {
            AcquireTarget();
        }

        private void AcquireTarget()
        {
            //if enemy in range
            if (targets.Count > 0)
            {
                //attack enemy
                Vector3 currentTarget = targets[0].transform.position;
                Vector3 targetDirection = (currentTarget - transform.position).normalized;
                AttackTarget(targetDirection);
            }
        }

        public abstract void AttackTarget(Vector3 targetDirection);

        public abstract void StopAttacking();


        //detect when mechs enter or exit range

        protected void OnTriggerEnter(Collider other)
        {
            var isEnemy = other.transform.GetComponent<Enemy>();
            if (isEnemy != null)
                targets.Add(isEnemy);
        }
  
        protected void OnTriggerExit(Collider other)
        {
            var isEnemy = other.transform.GetComponent<Enemy>();
            if (isEnemy != null)
            {
                //check for enemy in queue and remove
                int i = 0;
                while (i < targets.Count)
                {
                    if (targets[i].GetID() == isEnemy.GetID())
                    {
                        targets.Remove(isEnemy);
                        //check if no more enemies in queue and stop attacking
                        if (targets.Count == 0)
                            StopAttacking();
                        return;
                    }
                    i++;
                }
            }
        }

        public void EnableCollider()
        {
            targetCollider.enabled = true;
        }

        public int WarFundsRequired()
        {
            return warFundsCost;
        }
    }
}
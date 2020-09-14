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


        protected void Start()
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
                print("attacking");
                //attack enemy
                Vector3 currentTarget = targets[targets.Count -1].transform.position;
                Vector3 targetDirection = (currentTarget - transform.position).normalized;
                AttackTarget(targetDirection);
            }
        }

        public abstract void AttackTarget(Vector3 targetDirection);


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
                foreach (Enemy target in targets)
                {
                    if (target.transform.name == isEnemy.transform.name)
                        targets.Remove(target);
                    return;
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
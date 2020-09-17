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

        protected List<GameObject> targets = new List<GameObject>();
        protected SphereCollider targetCollider;


        protected virtual void Start()
        {
            targetCollider = transform.GetComponent<SphereCollider>();
            targetCollider.radius = towerRange;
        }

        protected abstract void AttackTarget(Vector3 targetDirection);

        protected abstract void StopAttacking();


        //detect when mechs enter or exit range

        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "CrabMech" || other.CompareTag("RunMech")) //Ask Jon any difference?
            {
                targets.Add(other.gameObject);
            }
        }

        protected void OnTriggerStay(Collider other)
        {
            //if enemy in range
            if (targets.Count > 0)
            {
                //Attack enemy
                AcquireTarget();
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.tag == "CrabMech" || other.CompareTag("RunMech"))
            {
                targets.Remove(other.gameObject);
                if (targets.Count == 0)
                    StopAttacking();
            }
        }

        protected void AcquireTarget()
        {
            //check target isnt dead
            int targetCount = 0;
            GameObject targetToCheck = targets[targetCount];
            while (targetToCheck.GetComponent<Enemy>().IsAlive() == false && targetCount < targets.Count)
            {
                //if dead cycle to next target
                targetCount++;
                //unless no more targets
                if (targetCount >= targets.Count)
                {
                    StopAttacking();
                    return;
                }
                targetToCheck = targets[targetCount];
            }

            Vector3 currentTarget = targetToCheck.transform.position;
            Vector3 targetDirection = (currentTarget - transform.position).normalized;
            AttackTarget(targetDirection);
        }

        public void EnableCollider()
        {
            targetCollider.enabled = true;
        }

        public int WarFundsRequired()
        {
            return warFundsCost;
        }

        public void RemoveFromPool (GameObject toRemove)
        {
            targets.Remove(toRemove);
        }
    }
}
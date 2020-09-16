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

        //protected List<Enemy> targets = new List<Enemy>();
        protected List<GameObject> _targets = new List<GameObject>();
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
                _targets.Add(other.gameObject);
            }
        }

        protected void OnTriggerStay(Collider other)
        {
            //if enemy in range
            if (_targets.Count > 0)
            {
                //Attack enemy
                AcquireTarget();
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.tag == "CrabMech" || other.CompareTag("RunMech"))
            {
                _targets.Remove(other.gameObject);
                if (_targets.Count == 0)
                    StopAttacking();
            }
        }

        protected void AcquireTarget()
        {
            Vector3 currentTarget = _targets[0].transform.position;
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
    }
}
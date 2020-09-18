using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField]
        protected int warFundsCost;
        [SerializeField]
        protected int towerRange;
        [SerializeField]
        Sprite _buttonImage;

        //protected List<GameObject> targets = new List<GameObject>();
        protected List<Enemy> targets = new List<Enemy>();
        protected SphereCollider targetCollider;

        protected Enemy _currentTarget;

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
                targets.Add(other.GetComponent<Enemy>());
                _currentTarget = other.GetComponent<Enemy>();
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
                targets.Remove(other.GetComponent<Enemy>());
                if (targets.Count == 0)
                    StopAttacking();
            }
        }

        protected void AcquireTarget()
        {
            //foreach (Enemy target in targets)
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].IsAlive() != true)
                    targets.Remove(targets[i]);
            }

            if (targets.Count == 0)
                StopAttacking();
            else
            {
                Vector3 currentTarget = targets[0].gameObject.transform.position;
                Vector3 targetDirection = (currentTarget - transform.position).normalized;
                AttackTarget(targetDirection);
            }
        }

        public void EnableCollider()
        {
            targetCollider.enabled = true;
        }

        public int GetWarFundsRequired()
        {
            return warFundsCost;
        }

        public Sprite GetButtonImage()
        {
            return _buttonImage;
        }
    }
}
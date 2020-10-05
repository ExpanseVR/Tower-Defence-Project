using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Tower : MonoBehaviour
    {
        public enum MissileType
        {
            Normal,
            Homing
        }

        [SerializeField]
        int _ID;
        
        [SerializeField]
        protected int warFundsCost;
        [SerializeField]
        protected int towerRange;
        [SerializeField]
        Sprite _buttonImageUI;
        [SerializeField]
        Sprite _sellImageUI;

        [SerializeField]
        protected int _warFundSellAmount;

        [SerializeField]
        Tower _upgradeTowerLevelOne;

        [SerializeField]
        protected float _rotateSpeed;

        //protected List<GameObject> targets = new List<GameObject>();
        protected List<Enemy> targets = new List<Enemy>();
        protected SphereCollider targetCollider;

        protected Enemy _currentTarget;

        int _frames = 0;

        public virtual void OnEnable()
        {
            targetCollider = transform.GetComponent<SphereCollider>();
            targetCollider.radius = towerRange;
        }

        protected abstract void AttackTarget(Vector3 targetDirection);

        protected abstract void StopAttacking();

        //detect when mechs enter or exit range

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CrabMech") || other.CompareTag("RunMech")) //Ask Jon any difference?
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
                _frames++;
                //every 10 frames;
                if (_frames % 10 == 0)
                {
                    //Attack enemy
                    AcquireTarget();
                }
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CrabMech") || other.CompareTag("RunMech"))
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

        public void SetCollider(bool colliderState)
        {
            targetCollider.enabled = colliderState;
        }

        public int GetWarFundsRequired()
        {
            return warFundsCost;
        }

        public Sprite GetButtonImage()
        {
            return _buttonImageUI;
        }

        public void GetUpgradeDetails(out int ID, out int cost)
        {
            ID = this._ID;
            cost = _upgradeTowerLevelOne.GetWarFundsRequired();
        }

        public GameObject GetUpgradeTower ()
        {
            if (_upgradeTowerLevelOne != null)
                return _upgradeTowerLevelOne.gameObject;
            else
                return null;
        }

        public void GetSellDetails(out Sprite sellImage, out int cost)
        {
            sellImage = _sellImageUI;
            cost = _warFundSellAmount;
        }
    }
}
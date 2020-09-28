using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts;

namespace GameDevHQ.Towers
{
    public class Missle_Launcher : Tower
    {
        [SerializeField]
        private GameObject _missilePrefab; //holds the missle gameobject to clone
        [SerializeField]
        private MissileType _missileType; //type of missle to be launched
        [SerializeField]
        private GameObject[] _misslePositionsLeft; //array to hold the rocket positions on the turret
        [SerializeField]
        private GameObject[] _misslePositionsRight; //array to hold the rocket positions on the turret
        [SerializeField]
        private float _fireDelay; //fire delay between rockets
        [SerializeField]
        private float _launchSpeed; //initial launch speed of the rocket
        [SerializeField]
        private float _power; //power to apply to the force of the rocket
        [SerializeField]
        private float _fuseDelay; //fuse delay before the rocket launches
        [SerializeField]
        private float _reloadTime; //time in between reloading the rockets
        [SerializeField]
        private float _destroyTime = 10.0f; //how long till the rockets get cleaned up
        private bool _launched; //bool to check if we launched the rockets

        [SerializeField]
        GameObject _turret; //Part of tower to rotate towards enemy

        [SerializeField]
        private int _missileDamage;

        private int _missilePosRightCount;
        GameObject[] _missilePoolRight;
        private int _missilePosLeftCount;
        GameObject[] _missilePoolLeft;

        public override void OnEnable()
        {
            base.OnEnable();
            _missilePosRightCount = 6;
            _missilePoolRight = new GameObject[_misslePositionsRight.Length];
            _missilePosLeftCount = 6;
            _missilePoolLeft = new GameObject[_misslePositionsLeft.Length];
        }

        protected override void AttackTarget(Vector3 targetDirection)
        {
            targetDirection.y = 0;
            _turret.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            if (_launched == false)
            {
                _launched = true; //set the launch bool to true
                StartCoroutine(FireRocketsRoutine());
            }
        }

        protected override void StopAttacking()
        {

        }

        IEnumerator FireRocketsRoutine()
        {
            {
                _missilePosLeftCount = MissileLocationCycle(_missilePosLeftCount, _misslePositionsLeft);
                _missilePosRightCount = MissileLocationCycle(_missilePosRightCount, _misslePositionsRight);

                LoadRocket(_missilePosLeftCount, _missilePoolLeft, _misslePositionsLeft);
                LoadRocket(_missilePosRightCount, _missilePoolRight, _misslePositionsRight);
                yield return new WaitForSeconds(_fireDelay); //wait for the firedelay

                _misslePositionsLeft[_missilePosLeftCount].SetActive(true);
                _misslePositionsRight[_missilePosRightCount].SetActive(true);

                _launched = false; //set launch bool to false
            }
        }

        private int MissileLocationCycle(int missileCount, GameObject[] missilePositions)
        {
            missileCount++;
            if (missileCount >= missilePositions.Length)
                missileCount = 0;

            return missileCount;
        }

        private void LoadRocket(int rocketLocation, GameObject[] missilePool, GameObject[] missilePositions)
        {
            if (missilePool[rocketLocation] == null)
            {
                missilePool[rocketLocation] = Instantiate(_missilePrefab) as GameObject; //instantiate a rocket
                missilePool[rocketLocation].transform.parent = missilePositions[rocketLocation].transform; //set the rockets parent to the missle launch position
            }
            else
            {
                missilePool[rocketLocation].transform.position = missilePool[rocketLocation].transform.position;
                missilePool[rocketLocation].SetActive(true);
            }
            missilePool[rocketLocation].transform.localPosition = Vector3.zero; //set the rocket position values to zero
            missilePool[rocketLocation].transform.localEulerAngles = new Vector3(0, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction
            missilePool[rocketLocation].GetComponent<Missle>().AssignMissleRules(_missileType, targets[0].gameObject.transform, _launchSpeed, _power, _fuseDelay, _missileDamage);

            //_misslePositions[rocketLocation].SetActive(false); //hide missile in place to look like it shoots;
        }
    }
}

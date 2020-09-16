using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.FileBase.Missle_Launcher.Missle;
using GameDevHQ.Scripts;

namespace GameDevHQ.FileBase.Missle_Launcher
{
    public class Missle_Launcher : Tower
    {
        [SerializeField]
        private GameObject _missilePrefab; //holds the missle gameobject to clone
        [SerializeField]
        private GameObject[] _misslePositions; //array to hold the rocket positions on the turret
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
        [SerializeField]
        GameObject _turret; //Part of tower to rotate towards enemy
        private bool _launched; //bool to check if we launched the rockets

        private int _missilePosCount;
        GameObject[] _missilePool;
        //TODO: Switch _missilePool to ObjectPool

        private void OnEnable()
        {
            _missilePosCount = 6;
            _missilePool = new GameObject[_misslePositions.Length];
        }

        protected override void AttackTarget(Vector3 targetDirection)
        {
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

        private void MissileLocationCycle()
        {
            _missilePosCount++;
            if (_missilePosCount >= _misslePositions.Length)
                _missilePosCount = 0;
        }

        IEnumerator FireRocketsRoutine()
        {
            MissileLocationCycle();
            LoadRocket(_missilePosCount);        
            yield return new WaitForSeconds(_fireDelay); //wait for the firedelay
            _misslePositions[_missilePosCount].SetActive(true);

            _launched = false; //set launch bool to false
        }

        private void LoadRocket(int rocketLocation)
        {
            GameObject newMissile = Instantiate(_missilePrefab) as GameObject; //instantiate a rocket
            newMissile.transform.parent = _misslePositions[rocketLocation].transform; //set the rockets parent to the missle launch position
             
            newMissile.transform.localPosition = Vector3.zero; //set the rocket position values to zero
            newMissile.transform.localEulerAngles = new Vector3(-90, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction
            newMissile.GetComponent<GameDevHQ.FileBase.Missle_Launcher.Missle.Missle>().AssignMissleRules(_launchSpeed, _power, _fuseDelay, _destroyTime, targets[0]);
            _missilePool[rocketLocation] = newMissile;
            //_misslePositions[rocketLocation].SetActive(false); //hide missile in place to look like it shoots;
        }
    }
}
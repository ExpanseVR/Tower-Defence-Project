using GameDevHQ.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevHQ.Scripts
{
    public class TowerPlacer : MonoBehaviour
    {
        public static event Action MouseOver;

        [SerializeField]
        GameObject _particles;

        ObjectPool _towerPool = new ObjectPool();
        GameObject _currentTower;

        private bool _isActivated = false;
        private bool _isTowerPlaced = false;
        private bool _isMouseOver = false;

        // Start is called before the first frame update
        void Start()
        {
            TowerManager.PlaceTower += PlaceTower;
            TowerManager.ActivatePlaceHolders += Activate;
            TowerManager.Reset += DeActivate;
            _particles.SetActive(false);
        }

        //prview towers when mouse is over object
        private void Activate() //Better name?
        {
            _isActivated = true;
            if (!_isTowerPlaced)
            {
                SetTower();
                _particles.SetActive(!_particles.activeSelf);
            }
        }

        private void SetTower()
        {
            //get current tower being held
            GameObject towerHeld = TowerManager.Instance.GetTower();

            //check if there is already a tower
            _currentTower = _towerPool.CheckForDisabledGameObject(towerHeld);
            if (_currentTower == null)
            {
                //if not instantiate it
                _currentTower = Instantiate(towerHeld, this.transform.position, Quaternion.identity);
                _currentTower.transform.SetParent(this.transform);
                _towerPool.AddNewObject(_currentTower);
            }
            _currentTower.SetActive(false);
        }

        private void DeActivate()
        {
            _isActivated = false;
            _particles.SetActive(false);
            if (!_isTowerPlaced)
                _currentTower.SetActive(false);
        }

        private void OnMouseEnter()
        {
            //cant place on existing tower
            if (!_isTowerPlaced && _isActivated)
            {
                _currentTower.SetActive(true);
                _isMouseOver = true;
                MouseOver();
            }
        }

        private void OnMouseExit()
        {
            if (!_isTowerPlaced && _isActivated)
            {
                _currentTower.SetActive(false);
                _isMouseOver = false;
                MouseOver();
            }
        }

        private void PlaceTower ()
        {
            if (_isTowerPlaced == false && _isMouseOver)
            {
                _isTowerPlaced = true;
                _particles.SetActive(false);
            }
        }
    }
}
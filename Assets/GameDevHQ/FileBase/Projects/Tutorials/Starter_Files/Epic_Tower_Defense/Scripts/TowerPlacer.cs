using GameDevHQ.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameDevHQ.Scripts
{
    public class TowerPlacer : MonoBehaviour
    {
        public static event Action MouseOver;

        [SerializeField]
        GameObject _particles;

        //ObjectPool _towerPool = new ObjectPool();
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

        //preview towers when mouse is over object
        private void Activate() //Better name?
        {
            _isActivated = true;
            if (!_isTowerPlaced)
            {
                //SetTower();
                _particles.SetActive(!_particles.activeSelf);
            }
        }

        private void DeActivate()
        {
            _particles.SetActive(false);
        }

        private void OnMouseEnter()
        {
            //cant place on existing tower
            if (!_isTowerPlaced && _isActivated)
            {
                MouseOver();
                _currentTower = TowerManager.Instance.GetTower();
                _currentTower.transform.position = this.transform.position;
                _isMouseOver = true;

            }
        }

        private void OnMouseExit()
        {
            if (!_isTowerPlaced && _isActivated)
            {
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
            _isActivated = false;
        }
    }
}
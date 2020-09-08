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
        GameObject[] _towers;

        [SerializeField]
        GameObject _particles;

        private bool _isActivated = false;
        private bool _isTowerPlaced = false;
        private bool _isMouseOver = false;

        private void OnEnable()
        {
            _towers[0].SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            TowerManager.PlaceTower += PlaceTower;
            TowerManager.ActivatePlaceHolders += Activate;
            TowerManager.Reset += DeActivate;
            _particles.SetActive(false);
            _towers[0].SetActive(false);
        }

        //prview towers when mouse is over object
        private void Activate() //Better name?
        {
            _isActivated = true;
            if (!_isTowerPlaced) 
                _particles.SetActive(!_particles.activeSelf);
        }

        private void DeActivate()
        {
            _isActivated = false;
            _particles.SetActive(false);
            if (!_isTowerPlaced)
                _towers[0].SetActive(false);
        }

        private void OnMouseEnter()
        {
            //cant place on existing tower
            if (!_isTowerPlaced && _isActivated)
            {
                _towers[0].SetActive(true);
                _isMouseOver = true;
                MouseOver();
            }
        }

        private void OnMouseExit()
        {
            if (!_isTowerPlaced && _isActivated)
            {
                _towers[0].SetActive(false);
                _isMouseOver = false;
                MouseOver();
            }
        }

        private void PlaceTower ()
        {
            print("placing");
            if (_isTowerPlaced == false && _isMouseOver)
            {
                _isTowerPlaced = true;
                _particles.SetActive(false);
            }
        }
    }
}
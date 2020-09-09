﻿using GameDevHQ.Scripts.Utility;
using UnityEngine;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        public static event Action ActivateTowerZones; //Better name?
        public static event Action PlaceTower;
        public static event Action Reset;

        [SerializeField]
        GameObject _towerToPlace;

        ObjectPool _towerPool = new ObjectPool();

        GameObject _activeTowerHeld;
        //bool _instantiatedTower = false;
        bool _heldTowerIsActive = false;
        bool _canPlaceTower = false;

        private void Start()
        {
            TowerPlacementZone.MouseOver += CanPlace; 
        }

        // Update is called once per frame
        void Update()
        {
            //when build option is selected
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_heldTowerIsActive)
                    return;
                //tower appears
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    _activeTowerHeld = _towerPool.CheckForDisabledGameObject(_towerToPlace);
                    if (_activeTowerHeld == null)
                    {
                        _activeTowerHeld = Instantiate(_towerToPlace);
                        _towerPool.AddNewObject(_activeTowerHeld);
                    }
                    _activeTowerHeld.SetActive(true);
                    _activeTowerHeld.transform.position = this.transform.position;
                    _heldTowerIsActive = true;
                }
                //particle effect at available locactions
                if (ActivateTowerZones != null)
                    ActivateTowerZones();
            }

            HaveATowerToPlace();
            
            MouseInput();
        }

        private void MouseInput()
        {
            //left click to place tower
            if (Input.GetMouseButtonDown(0))
            {
                if (_canPlaceTower)
                {
                    int newTowerCost = _towerToPlace.GetComponent<Tower>().GetWarFundCost();
                    //if enough warFunds
                    if (GameManger.Instance.GetWarfunds() > newTowerCost)
                        GameManger.Instance.SetWarFunds(-newTowerCost);
                    else
                        return;

                    if (PlaceTower != null)
                    {
                        PlacingTower();
                        PlaceTower();
                        _canPlaceTower = false;
                    }
                }
            }

            //right click cancels placement
            if (Input.GetMouseButtonDown(1))
            {
                if (_heldTowerIsActive)
                    CancelPlacement();
            }
        }

        private void HaveATowerToPlace()
        {
            if (_heldTowerIsActive == true)
            {
                //and follows mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit);
                _activeTowerHeld.transform.position = rayHit.point;
                //area of effect is red when not over predifinedArea
                //& predefined areas turn green
            }
        }

        //turret snaps to area when mouse over predefined area
        private void CanPlace()
        {
            if (_activeTowerHeld != null)
            {
                _heldTowerIsActive = !_heldTowerIsActive;
                _canPlaceTower = !_canPlaceTower;
            }
        }


        private void CancelPlacement()
        {
            if (ActivateTowerZones != null)
                Reset(); //Reset any active available tower placement spots

            _activeTowerHeld.SetActive(false);
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        private void PlacingTower()
        {
            _activeTowerHeld.transform.GetComponent<RangeColour>().SetRange(false);

            if (ActivateTowerZones != null)
                Reset(); //Reset any active available tower placement spots
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        public GameObject GetTower()
        {
            return _activeTowerHeld;
        }
    }
}
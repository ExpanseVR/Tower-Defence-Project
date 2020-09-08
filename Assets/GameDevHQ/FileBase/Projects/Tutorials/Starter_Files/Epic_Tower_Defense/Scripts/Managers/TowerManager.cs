using GameDevHQ.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameDevHQ.FileBase.Gatling_Gun;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        public static event Action ActivatePlaceHolders; //Better name?
        public static event Action PlaceTower;
        public static event Action Reset;

        [SerializeField]
        GameObject _towerToPlace;

        GameObject _towerHolding;
        bool _instantiatedTower = false;
        bool _heldTowerIsActive = false;
        bool _canPlaceTower = false;

        private void Start()
        {
            TowerPlacer.MouseOver += CanPlace; 
        }

        // Update is called once per frame
        void Update()
        {
            //when build option is selected
            if (Input.GetKeyDown(KeyCode.T))
            {
                //tower appears
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    if (_instantiatedTower == false)
                    {
                        _towerHolding = Instantiate(_towerToPlace, rayHit.point, Quaternion.identity);
                        _instantiatedTower = true;
                    }
                    else
                    {
                        _towerHolding.transform.position = rayHit.point;
                        _towerHolding.SetActive(true);
                    }
                    _heldTowerIsActive = true;
                }
                //particle effect at available locactions
                if (ActivatePlaceHolders != null)
                    ActivatePlaceHolders();
            }

            HaveATowerToPlace();
            
            MouseInput();
        }

        private void MouseInput()
        {
            //left click to place tower
            if (Input.GetMouseButtonDown(0))
            {
                int newTowerCost = _towerToPlace.GetComponent<Tower>().GetWarFundCost();
                //if enough warFunds
                if (GameManger.Instance.GetWarfunds() > newTowerCost)
                {
                    GameManger.Instance.SetWarFunds(-newTowerCost);
                }
                else
                    _canPlaceTower = false;

                if (_heldTowerIsActive == true && _canPlaceTower)
                {
                    if (PlaceTower != null)
                    {
                        PlaceTower();
                        CancelPlacement();
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
                _towerHolding.transform.position = rayHit.point;
                //area of effect is red when not over predifinedArea
                //& predefined areas turn green
            }
        }

        //turret snaps to area when mouse over predefined area
        private void CanPlace()
        {
            if (_towerHolding != null)
            {
                _towerHolding.SetActive(!_towerHolding.activeSelf);
                _canPlaceTower = !_canPlaceTower;
            }
        }


        private void CancelPlacement()
        {
            if (ActivatePlaceHolders != null)
                Reset(); //Reset any active available tower placement spots

            if (_instantiatedTower == true)
            {
                _towerHolding.SetActive(false); //if there is an instantiated tower deactivate it for later use;
            }

            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        public GameObject GetTower()
        {
            return _towerToPlace;
        }
    }
}
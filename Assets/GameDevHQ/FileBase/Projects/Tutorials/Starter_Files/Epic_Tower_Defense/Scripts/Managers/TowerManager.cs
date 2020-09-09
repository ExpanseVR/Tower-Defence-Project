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

        ObjectPool _towerPool = new ObjectPool();

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
                if (_heldTowerIsActive)
                    return;
                //tower appears
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    _towerHolding = _towerPool.CheckForDisabledGameObject(_towerToPlace);
                    if (_towerHolding == null)
                    {
                        _towerHolding = Instantiate(_towerToPlace);
                        _towerPool.AddNewObject(_towerHolding);
                    }
                    _towerHolding.SetActive(true);
                    _towerHolding.transform.position = this.transform.position;
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

                if (_canPlaceTower)
                {
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
                _heldTowerIsActive = !_heldTowerIsActive;
                _canPlaceTower = !_canPlaceTower;
            }
        }


        private void CancelPlacement()
        {
            if (ActivatePlaceHolders != null)
                Reset(); //Reset any active available tower placement spots

            _towerHolding.SetActive(false);
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        private void PlacingTower()
        {
            _towerHolding.transform.GetComponent<RangeColour>().SetRange(false);

            if (ActivatePlaceHolders != null)
                Reset(); //Reset any active available tower placement spots
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        public GameObject GetTower()
        {
            return _towerHolding;
        }
    }
}
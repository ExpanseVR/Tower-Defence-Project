using GameDevHQ.Scripts.Utility;
using UnityEngine;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        ObjectPool _towerPool = new ObjectPool();
        GameObject _towerSelected;
        TowerPlacementZone _towerPlacementZone;

        bool _gamePlaying = true;
        bool _heldTowerIsActive = false;
        bool _overTowerPlacementZone = false;
        bool _canPlaceTower = true;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), (Action<bool>)OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.UIArmorySelected.ToString(), (Action<GameObject>)TowerSelected);
            EventManager.Listen(EventManager.Events.GamePlaying.ToString(), (Action<bool>)IsGamePlaying);
        }

        // Update is called once per frame
        void Update()
        {
            //check if game is paused
            if (!_gamePlaying)
                return;

            HaveATowerToPlace();
            MouseInput();
            if (Input.GetKeyDown(KeyCode.D))
            {
                EventManager.Fire(EventManager.Events.MouseOverTowerZone.ToString(), true);
            }
        }

        private void MouseInput()
        {
            //left click to place tower
            if (Input.GetMouseButtonDown(0))
            {
                if (_overTowerPlacementZone)
                {
                    if (_canPlaceTower && _towerSelected != null)
                    {
                        int newTowerCost = _towerSelected.GetComponent<Tower>().GetWarFundsRequired();

                        //if enough warFunds
                        if (GameManger.Instance.GetWarfunds() >= newTowerCost)
                            GameManger.Instance.SetWarFunds(-newTowerCost);
                        else
                            return;

                        PlacingTower();
                    }
                    else if (!_canPlaceTower && _towerSelected == null && _towerPlacementZone.GetCurrentTower() != null)
                        EventManager.Fire(EventManager.Events.UIUpgradeMenu.ToString());
                }
            }

            //right click cancels placement
            if (Input.GetMouseButtonDown(1))
            {
                if (_heldTowerIsActive)
                    CancelPlacement();
                if (_overTowerPlacementZone && !_canPlaceTower && _towerSelected == null)
                    UIManager.Instance.RefundMenuUI();
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
                _towerSelected.transform.position = rayHit.point;
            }
        }

        //turret snaps to area when mouse over predefined area
       
        private void OverTowerPlacementZone(bool towerPlaced)
        {
            if (_towerSelected != null && towerPlaced == false)
            {
                _heldTowerIsActive = !_heldTowerIsActive;
                _canPlaceTower = true;
            }
            else
                _canPlaceTower = false;

            _overTowerPlacementZone = !_overTowerPlacementZone;
        }

        private void CancelPlacement()
        {
            EventManager.Fire(EventManager.Events.ResetTowerZones.ToString());
            _towerSelected.SetActive(false);
            _heldTowerIsActive = false;
            _overTowerPlacementZone = false;
        }

        private void PlacingTower()
        {
            _heldTowerIsActive = false;
            //_overTowerPlacementZone = false;
            _towerSelected = null;

            EventManager.Fire(EventManager.Events.PlaceTower.ToString());
            EventManager.Fire(EventManager.Events.ResetTowerZones.ToString());
        }

        public GameObject GetTower()
        {
            return _towerSelected;
        }

        public void TowerSelected(GameObject selectedTower)
        {
            if (_heldTowerIsActive)
                return;
            //tower appears
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                _towerSelected = GetNewTower(selectedTower, rayHit.point, this.transform);
                _heldTowerIsActive = true;
            }
            //particle effect at available locactions
            EventManager.Fire(EventManager.Events.ActivateTowerZones.ToString());
        }

        public GameObject GetNewTower (GameObject selectedTower, Vector3 startPos, Transform towerParent)
        {
            GameObject newTower;
            newTower = _towerPool.GetGameObjectFromPool(selectedTower);
            newTower.transform.position = startPos;
            newTower.transform.parent = towerParent;
            newTower.SetActive(true);
            return newTower;
        }

        public void TowerPlaced(TowerPlacementZone towerPlacementZone)
        {
            this._towerPlacementZone = towerPlacementZone;
        }

        public void ResetTower (Tower towerToReset, bool placementZoneInUse)
        {
            //reset tower placement zone
            _towerPlacementZone.Reset(placementZoneInUse);
            //reset tower collider, activate range FX and deactivate tower (to go back into pool)
            towerToReset.SetCollider(false);
            
            RangeColour rangeColour = towerToReset.GetComponent<RangeColour>(); //To refactor GetComponent Call
            if (rangeColour != null)
                rangeColour.SetRange(true);

            towerToReset.gameObject.SetActive(false);
        }

        public void IsGamePlaying (bool isPlaying)
        {
            _gamePlaying = isPlaying;
        }


        private void OnDisable()
        {
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), (Action<bool>)OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.UIArmorySelected.ToString(), (Action<GameObject>)TowerSelected);
            EventManager.Listen(EventManager.Events.GamePlaying.ToString(), (Action<bool>)IsGamePlaying);
        }

    }
}
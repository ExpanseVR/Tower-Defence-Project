using GameDevHQ.Scripts.Utility;
using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        ObjectPool _towerPool = new ObjectPool();
        GameObject _towerSelected;
        TowerPlacementZone _towerPlacementZone;

        bool _heldTowerIsActive = false;
        bool _overTowerPlacementZone = false;

        bool towerPlaced = false; //TOREMOVE

        private void OnEnable()
        {
            //EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), (Action<bool>)OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.UIArmorySelected.ToString(), (Action<GameObject>)TowerSelected);
        }

        // Update is called once per frame
        void Update()
        {
            HaveATowerToPlace();
            MouseInput();
        }

        private void MouseInput()
        {
            //left click to place tower
            if (Input.GetMouseButtonDown(0))
            {
                if (_overTowerPlacementZone)
                {
                    if (towerPlaced == false && _towerSelected != null)
                    {
                        int newTowerCost = _towerSelected.GetComponent<Tower>().GetWarFundsRequired();

                        //if enough warFunds
                        if (GameManger.Instance.GetWarfunds() >= newTowerCost)
                            GameManger.Instance.SetWarFunds(-newTowerCost);
                        else
                            return;

                        PlacingTower();
                    }
                    else if (towerPlaced == true)
                        EventManager.Fire(EventManager.Events.UIUpgradeMenu.ToString());
                }
            }

            //right click cancels placement
            if (Input.GetMouseButtonDown(1))
            {
                if (_heldTowerIsActive)
                    CancelPlacement();
                UIManager.Instance.CancelUpgradeUI();
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
        /*private void OverTowerPlacementZone(bool towerPlaced)
        {
            print("TowerManager :: OverTowerPlacementZone bool is: " + towerPlaced);
            if (_towerSelected != null && towerPlaced == false)
            {
                _heldTowerIsActive = !_heldTowerIsActive;
                _canPlaceTower = !_canPlaceTower;
            }
        }*/
        
        private void OverTowerPlacementZone()
        {
            if (_towerSelected != null && towerPlaced == false)
                _heldTowerIsActive = !_heldTowerIsActive;
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

        public void TowerPlaced(TowerPlacementZone towerPlacementZone, bool isTowerPlaced)
        {
            this._towerPlacementZone = towerPlacementZone;
            towerPlaced = isTowerPlaced;
        }

        public void ResetTower (Tower towerToReset, bool placementZoneInUse)
        {
            //reset tower placement zone
            _towerPlacementZone.Reset(placementZoneInUse);
            //reset tower collider, activate range FX and deactivate tower (to go back into pool)
            towerToReset.SetCollider(false);
            towerToReset.GetComponent<RangeColour>().SetRange(true); //To refactor GetComponent Call
            towerToReset.gameObject.SetActive(false);
        }


        private void OnDisable()
        {
            //EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), (Action<bool>)OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), OverTowerPlacementZone);
            EventManager.Listen(EventManager.Events.UIArmorySelected.ToString(), (Action<GameObject>)TowerSelected);
        }

    }
}
using GameDevHQ.Scripts.Utility;
using UnityEngine;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        ObjectPool _towerPool = new ObjectPool();
        GameObject _towerSelected;

        bool _heldTowerIsActive = false;
        bool _canPlaceTower = false;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), CanPlace);
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
                if (_canPlaceTower)
                {
                    int newTowerCost = _towerSelected.GetComponent<Tower>().GetWarFundsRequired();

                    //if enough warFunds
                    if (GameManger.Instance.GetWarfunds() >= newTowerCost)
                        GameManger.Instance.SetWarFunds(-newTowerCost);
                    else
                        return;

                    PlacingTower();
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
                _towerSelected.transform.position = rayHit.point;
            }
        }

        //turret snaps to area when mouse over predefined area
        private void CanPlace()
        {
            if (_towerSelected != null)
            {
                _heldTowerIsActive = !_heldTowerIsActive;
                _canPlaceTower = !_canPlaceTower;
            }
        }

        private void CancelPlacement()
        {
            EventManager.Fire(EventManager.Events.ResetTowerZones.ToString());
            _towerSelected.SetActive(false);
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        private void PlacingTower()
        {
            EventManager.Fire(EventManager.Events.PlaceTower.ToString());
            EventManager.Fire(EventManager.Events.ResetTowerZones.ToString());

            _heldTowerIsActive = false;
            _canPlaceTower = false;
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
                _towerSelected = _towerPool.GetGameObjectFromPool(selectedTower);
                _towerSelected.transform.parent = this.transform;
                _towerSelected.transform.position = rayHit.point;
                _towerSelected.SetActive(true);
                _heldTowerIsActive = true;
            }
            //particle effect at available locactions
            EventManager.Fire(EventManager.Events.ActivateTowerZones.ToString());
        }
        
        public GameObject GetTower()
        {
            return _towerSelected;
        }

        private void OnDisable()
        {
            EventManager.Listen(EventManager.Events.MouseOverTowerZone.ToString(), CanPlace);
            EventManager.Listen(EventManager.Events.UIArmorySelected.ToString(), (Action<GameObject>)TowerSelected);
        }
    }
}
using GameDevHQ.Scripts.Utility;
using UnityEngine;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        public static event Action onActivateTowerZones; //combine both of these and pass bool
        public static event Action onReset;

        public static event Action onPlaceTower;


        ObjectPool _towerPool = new ObjectPool();

        GameObject _towerSelected;
        bool _heldTowerIsActive = false;
        bool _canPlaceTower = false;

        private void OnEnable()
        {
            TowerPlacementZone.onMouseOver += CanPlace;
            UIManager.onArmorySelect += TowerSelected;
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
                    int newTowerCost = _towerSelected.GetComponent<Tower>().WarFundsRequired();

                    //if enough warFunds
                    if (GameManger.Instance.GetWarfunds() >= newTowerCost)
                        GameManger.Instance.SetWarFunds(-newTowerCost);
                    else
                        return;

                    if (onPlaceTower != null)
                    {
                        PlacingTower();
                        onPlaceTower();
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
                _towerSelected.transform.position = rayHit.point;
                //area of effect is red when not over predifinedArea
                //& predefined areas turn green
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
            if (onActivateTowerZones != null)
                onReset(); //Reset any active available tower placement spots

            _towerSelected.SetActive(false);
            _heldTowerIsActive = false;
            _canPlaceTower = false;
        }

        private void PlacingTower()
        {
            _towerSelected.transform.GetComponent<RangeColour>().SetRange(false);

            if (onActivateTowerZones != null)
                onReset(); //Reset any active available tower placement spots
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
            if (onActivateTowerZones != null)
                onActivateTowerZones();
        }
        
        public GameObject GetTower()
        {
            return _towerSelected;
        }

        private void OnDisable()
        {
            TowerPlacementZone.onMouseOver -= CanPlace;
            UIManager.onArmorySelect -= TowerSelected;
        }
    }
}
using GameDevHQ.FileBase.Gatling_Gun;
using GameDevHQ.Scripts.Managers;
using System;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class TowerPlacementZone : MonoBehaviour
    {
        public static event Action onMouseOver;

        [SerializeField]
        GameObject _particles;

        GameObject _currentTower;

        private bool _isActivated = false;
        private bool _isTowerPlaced = false;
        private bool _isMouseOver = false;

        private void OnEnable()
        {
            TowerManager.onPlaceTower += PlaceTower;
            TowerManager.onActivateTowerZones += Activate;
            TowerManager.onReset += DeActivate;
        }

        // Start is called before the first frame update
        void Start()
        {
            _particles.SetActive(false);
        }

        //available places start particle system
        //and can have MouseOver interaction
        private void Activate()
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
                if (onMouseOver != null)
                    onMouseOver();
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
                if (onMouseOver != null)
                    onMouseOver();
            }
        }

        private void PlaceTower ()
        {
            if (_isTowerPlaced == false && _isMouseOver)
            {
                _isTowerPlaced = true;
                _currentTower.transform.parent = this.transform;
                _currentTower.GetComponent<Tower>().EnableCollider();
                _particles.SetActive(false);
            }
            _isActivated = false;
        }

        private void OnDisable()
        {
            TowerManager.onPlaceTower -= PlaceTower;
            TowerManager.onActivateTowerZones -= Activate;
            TowerManager.onReset -= DeActivate;
        }
    }
}
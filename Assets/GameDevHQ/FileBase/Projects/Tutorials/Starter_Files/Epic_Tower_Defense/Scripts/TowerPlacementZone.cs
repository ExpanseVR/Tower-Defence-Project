using GameDevHQ.FileBase.Gatling_Gun;
using GameDevHQ.Scripts.Managers;
using System;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class TowerPlacementZone : MonoBehaviour
    {
        [SerializeField]
        GameObject _particles;

        GameObject _currentTower;

        private bool _isActivated = false;
        private bool _isTowerPlaced = false;
        private bool _isMouseOver = false;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.PlaceTower.ToString(), PlaceTower);
            EventManager.Listen(EventManager.Events.ActivateTowerZones.ToString(), Activate);
            EventManager.Listen(EventManager.Events.ResetTowerZones.ToString(), DeActivate);
        }

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
                _particles.SetActive(!_particles.activeSelf);
        }

        private void DeActivate()
        {
            _particles.SetActive(false);
        }

        private void OnMouseEnter()
        {
            //check if available spot to place tower
            if (_isActivated)
            {
                //check if tower already there
                if (!_isTowerPlaced)
                {
                    EventManager.Fire(EventManager.Events.MouseOverTowerZone.ToString());
                    _currentTower = TowerManager.Instance.GetTower();
                    _currentTower.transform.position = this.transform.position;
                    _isMouseOver = true;
                }
                else
                {
                    //check if can upgrade
                }
            }
        }

        private void OnMouseExit()
        {
            if (!_isTowerPlaced && _isActivated)
            {
                _isMouseOver = false;
                EventManager.Fire(EventManager.Events.MouseOverTowerZone.ToString());
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
            EventManager.StopListening(EventManager.Events.PlaceTower.ToString(), PlaceTower);
            EventManager.StopListening(EventManager.Events.ActivateTowerZones.ToString(), Activate);
            EventManager.StopListening(EventManager.Events.ResetTowerZones.ToString(), DeActivate);
        }
    }
}
using GameDevHQ.Scripts;
using GameDevHQ.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameDevHQ.UI
{
    public class PopUpMenu : MonoBehaviour
    {
        private enum PopUpMenuType {
            UpgradeTower,
            SellTower
        }

        [SerializeField]
        Sprite[] _menuBackgrounds;

        [SerializeField]
        PopUpMenuType _menuType;

        [SerializeField]
        Image _menuImage;

        [SerializeField]
        Image _towerToRefundImage;

        [SerializeField]
        float _horizontalAdjustment;

        [SerializeField]
        float _verticalAdjustment;

        [SerializeField]
        private Text _costText;
        private int _transactionCost;

        [SerializeField]
        Button _checkButton;

        [SerializeField]
        Button _cancelButton;

        private RectTransform _menuUI;
        private Tower _tower;
        private TowerPlacementZone _towerPlacementZone;
        private int _towerID;

        private void OnEnable()
        {
            //OnClick.UpgradeTower += EnableButton;
        }

        void Start()
        {
            _menuUI = GetComponent<RectTransform>();
        }

        public void EnableMenu(TowerPlacementZone towerPlacementZone)
        {
            TowerDetails(towerPlacementZone);
            SetMenuPosition(_tower.gameObject.transform);
            if (_menuType == PopUpMenuType.UpgradeTower)
            {
                _tower.GetUpgradeDetails(out _towerID, out _transactionCost);
                _menuImage.sprite = _menuBackgrounds[_towerID];
                _costText.text = "$" + _transactionCost.ToString();
            }
            else
            {
                Sprite towerImage;
                _tower.GetSellDetails(out towerImage, out _transactionCost);
                _costText.text = "+ $" + _transactionCost.ToString();
                _towerToRefundImage.sprite = towerImage;
            }
            _menuImage.gameObject.SetActive(true);
        }

        private void TowerDetails(TowerPlacementZone towerPlacementZone)
        {
            this._towerPlacementZone = towerPlacementZone;
            _tower = towerPlacementZone.GetCurrentTower();
        }

        private void SetMenuPosition(Transform _targetTransform)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(_targetTransform.position);
            screenPoint.y += _horizontalAdjustment;
            screenPoint.x += _verticalAdjustment;
            _menuUI.position = screenPoint;
        }

        public void Upgrade()
        {
            //deduct warfund costs
            GameManger.Instance.SetWarFunds(-_transactionCost);
            //instantiate new tower
            GameObject upgradedTower = TowerManager.Instance.GetNewTower(_tower.GetUpgradeTower(), _towerPlacementZone.transform.position, _towerPlacementZone.transform);
            upgradedTower.GetComponent<Tower>().SetCollider(true);
            _towerPlacementZone.SetCurrentTower(upgradedTower);
            //reset old tower
            TowerManager.Instance.ResetTower(_tower, true);
            //close menu
            Close();
        }

        public void Sell()
        {
            //add warfunds
            GameManger.Instance.SetWarFunds(_transactionCost);
            //reset old tower
            TowerManager.Instance.ResetTower(_tower, false);
            Close();
        }

        public void Close()
        {
            EventManager.Fire(EventManager.Events.UIPopUpMenuClosed.ToString());
            _menuImage.gameObject.SetActive(false);
        }
    }
}
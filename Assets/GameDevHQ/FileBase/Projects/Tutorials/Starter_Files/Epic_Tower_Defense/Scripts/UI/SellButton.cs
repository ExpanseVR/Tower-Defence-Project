using GameDevHQ.Scripts;
using GameDevHQ.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class SellButton : MonoBehaviour
    {
        [SerializeField]
        Text _refundText;
        int _refund;

        [SerializeField]
        Image _towerButton;
        Sprite _sellImage;

        Tower _towerToUpgrade;

        public void SetButton(Tower towerToUpgrade)
        {
            this._towerToUpgrade = towerToUpgrade;
            _towerToUpgrade.GetSellDetails(out _sellImage, out _refund);
            _towerButton.sprite = _sellImage;
            _refundText.text = "+ $" + _refund.ToString();
        }

        public void SellTower()
        {
            UIManager.Instance.CancelUpgradeUI();
            TowerManager.Instance.ResetTower(_towerToUpgrade);
            GameManger.Instance.SetWarFunds(_refund);
        }
    }
}
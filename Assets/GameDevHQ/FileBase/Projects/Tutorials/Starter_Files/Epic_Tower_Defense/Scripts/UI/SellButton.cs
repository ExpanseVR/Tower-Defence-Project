using Boo.Lang.Environments;
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

        Tower _towerToSell;

        public void SetButton(Tower towerToSell)
        {
            //update button with selected tower sell options
            this._towerToSell = towerToSell;
            _towerToSell.GetSellDetails(out _sellImage, out _refund);
            _towerButton.sprite = _sellImage;
            _refundText.text = "+ $" + _refund.ToString();
        }

        public void SellTower()
        {
            //close upgrade UI
            UIManager.Instance.CancelUpgradeUI();
            //reset tower sold
            TowerManager.Instance.ResetTower(_towerToSell, false);
            //add refund to warfunds
            GameManger.Instance.SetWarFunds(_refund);
        }
    }
}
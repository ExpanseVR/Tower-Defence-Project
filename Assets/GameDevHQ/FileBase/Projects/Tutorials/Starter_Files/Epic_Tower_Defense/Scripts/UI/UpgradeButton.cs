using GameDevHQ.Scripts;
using GameDevHQ.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField]
        private Text _costText;
        private int _upgradeCost;

        [SerializeField]
        private Image _towerButton;     
        private Sprite buttonImage;

        private Tower _towerToUpgrade;
        private TowerPlacementZone _placementZone;

        public void SetButton (Tower upgradeTower, TowerPlacementZone placementZone)
        {
            //update button with selected tower upgrade option
            this._towerToUpgrade = upgradeTower;
            this._placementZone = placementZone;
            _towerToUpgrade.GetUpgradeDetails(out buttonImage, out _upgradeCost);
            _towerButton.sprite = buttonImage;
            _costText.text = "$" + _upgradeCost.ToString();
        }

        public void UpgradeTower ()
        {
            //check to see if you can afford upgrade
            if (GameManger.Instance.GetWarfunds() > _upgradeCost)
            {
                //close upgrade UI
                UIManager.Instance.CancelUpgradeUI();
                //reset current tower
                TowerManager.Instance.ResetTower(_towerToUpgrade, true);
                //deduct warfund costs
                GameManger.Instance.SetWarFunds(-_upgradeCost);
                //instantiate new tower
                GameObject upgradedTower = TowerManager.Instance.GetNewTower(_towerToUpgrade.GetUpgradeTower(), _placementZone.transform.position, _placementZone.transform);
                upgradedTower.GetComponent<Tower>().SetCollider(true);
            }
        }
    }
}
using GameDevHQ.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField]
        Text _costText;
        
        int cost;

        [SerializeField]
        Image _towerButton;
        
        Sprite buttonImage;

        public void SetButton (Tower upgradeTower)
        {
            upgradeTower.GetUpgradeDetails(out buttonImage, out cost);
            _towerButton.sprite = buttonImage;
            _costText.text = "$" + cost.ToString();
        }
    }
}
using GameDevHQ.Scripts.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;
using GameDevHQ.UI;

namespace GameDevHQ.Scripts.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField]
        Text _warFundsUI;

        [SerializeField]
        Text _waveCounterUI;

        [SerializeField]
        ArmoryButton[] _armoryButtons;

        [SerializeField]
        UpgradeButton _upgradeButton;

        [SerializeField]
        SellButton _sellButton;

        private GameObject _towerToUprade;

        private void OnEnable()
        {
            //Subscribe to EventsManger for UI interaction
            EventManager.Listen(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.Listen(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
            EventManager.Listen(EventManager.Events.UIUpgradeMenu.ToString(), UpgradeMenuUI);
        }

        private void Start()
        {
            _warFundsUI.text = GameManger.Instance.GetWarfunds().ToString();
        }

        private void UpdateWarFundsUI()
        {
            _warFundsUI.text = GameManger.Instance.GetWarfunds().ToString();
        }

        private void UpdateWaveCounterUI()
        {
            string waves = GameManger.Instance.GetWaveCount().ToString();
            string currentWaves = GameManger.Instance.GetCurrentWave().ToString();
            _waveCounterUI.text = (currentWaves + " / " + waves);
        }

        private void UpgradeMenuUI()
        {
            //change Armory menu to reflect upgrade and sale options
            _sellButton.gameObject.SetActive(true);
            _upgradeButton.gameObject.SetActive(true);

            Tower tower = _towerToUprade.GetComponent<Tower>();
            _upgradeButton.SetButton(tower);
            _sellButton.SetButton(tower);

            _armoryButtons[0].gameObject.SetActive(false);
            _armoryButtons[1].gameObject.SetActive(false);
        }

        public void CancelUpgradeUI()
        {
            _armoryButtons[0].gameObject.SetActive(true);
            _armoryButtons[1].gameObject.SetActive(true);
            _sellButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(false);
        }

        //get tower that may be upgraded
        public void WhichTowerPlaced(GameObject towerPlaced)
        {
            _towerToUprade = towerPlaced;
        }

        public void ArmorButton(GameObject armorSelected)
        {
            EventManager.Fire(EventManager.Events.UIArmorySelected.ToString(), armorSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.StopListening(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
            EventManager.StopListening(EventManager.Events.UIUpgradeMenu.ToString(), UpgradeMenuUI);
        }
    }
}
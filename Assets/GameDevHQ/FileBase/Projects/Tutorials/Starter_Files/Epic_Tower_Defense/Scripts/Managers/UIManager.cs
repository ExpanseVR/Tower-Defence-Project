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
        Text _livesUI;

        [SerializeField]
        Text _waveCounterUI;

        [SerializeField]
        ArmoryButton[] _armoryButtons;

        [SerializeField]
        PopUpMenu _upgradeMenu;

        [SerializeField]
        PopUpMenu _refundMenu;

        private GameObject _towerInZone;
        private TowerPlacementZone _placementZone;
        private ChangeUILayout _changeUILayout;
        private bool _popUpOpen = false;

        private void OnEnable()
        {
            //Subscribe to EventsManger for UI interaction
            EventManager.Listen(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.Listen(EventManager.Events.LivesChanged.ToString(), (Action<int>)UpdateLivesUI);
            EventManager.Listen(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
            EventManager.Listen(EventManager.Events.UIUpgradeMenu.ToString(), UpgradeMenuUI);
            EventManager.Listen(EventManager.Events.UIPopUpMenuClosed.ToString(), PopUpMenuClosed);
        }

        private void Start()
        {
            _changeUILayout = GetComponent<ChangeUILayout>();
            _warFundsUI.text = GameManger.Instance.GetWarfunds().ToString();
        }

        private void UpdateWarFundsUI()
        {
            _warFundsUI.text = GameManger.Instance.GetWarfunds().ToString();
        }

        private void UpdateLivesUI(int livesAdjustment)
        {
            _livesUI.text = livesAdjustment.ToString();

            //check for lives and update UI state:
            if (livesAdjustment > 60)
            {
                _changeUILayout.UpdateUI(PlayerStatesUI.PlayerStateType.Normal);
                return;
            }
            else if (livesAdjustment > 20)
            {
                _changeUILayout.UpdateUI(PlayerStatesUI.PlayerStateType.Caution);
                return;
            }
            _changeUILayout.UpdateUI(PlayerStatesUI.PlayerStateType.Warning);

        }

        private void UpdateWaveCounterUI()
        {
            string waves = GameManger.Instance.GetWaveCount().ToString();
            string currentWaves = GameManger.Instance.GetCurrentWave().ToString();
            _waveCounterUI.text = (currentWaves + " / " + waves);
        }

        private void UpgradeMenuUI()
        {
            //check another popup isnt open
            if (_popUpOpen == true)
                return;

            //check tower can be ugpraded
            if (_towerInZone.GetComponent<Tower>().GetUpgradeTower() != null)
            {
                _popUpOpen = true;
                _upgradeMenu.EnableMenu(_placementZone);
            }
        }

        public void RefundMenuUI()
        {
            //check another popup isnt open
            if (_popUpOpen == true)
                return;
            
            _popUpOpen = true;
            _refundMenu.EnableMenu(_placementZone);
        }

        public void PopUpMenuClosed()
        {
            _popUpOpen = false;
        }

        //get tower that may be upgraded
        public void WhichTowerPlaced(GameObject towerInZone, TowerPlacementZone placementZone)
        {
            this._placementZone = placementZone;
            this._towerInZone = towerInZone;
        }

        public void ArmorButton(GameObject armorSelected)
        {
            EventManager.Fire(EventManager.Events.UIArmorySelected.ToString(), armorSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.StopListening(EventManager.Events.LivesChanged.ToString(), (Action<int>)UpdateLivesUI);
            EventManager.StopListening(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
            EventManager.StopListening(EventManager.Events.UIUpgradeMenu.ToString(), UpgradeMenuUI);
            EventManager.StopListening(EventManager.Events.UIPopUpMenuClosed.ToString(), PopUpMenuClosed);
        }
    }
}
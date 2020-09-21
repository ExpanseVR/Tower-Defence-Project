using GameDevHQ.Scripts.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.Scripts.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField]
        Text _warFundsUI;

        [SerializeField]
        Text _waveCounterUI;

        private void OnEnable()
        {
            //Subscribe to EventsManger for UI interaction
            EventManager.Listen(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.Listen(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
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

        public void ArmorButton(GameObject armorSelected)
        {
            EventManager.Fire(EventManager.Events.UIArmorySelected.ToString(), armorSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.WarFundsChanged.ToString(), UpdateWarFundsUI);
            EventManager.StopListening(EventManager.Events.NewWaveStarted.ToString(), UpdateWaveCounterUI);
        }
    }
}
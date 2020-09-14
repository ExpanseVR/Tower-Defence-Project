using GameDevHQ.Scripts.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.Scripts.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public static event Action<GameObject> onArmorySelect;
        
        [SerializeField]
        Text _warFundsUI;

        [SerializeField]
        Text _waveCounterUI;

        private void OnEnable()
        {
            GameManger.onWarFundsChanged += UpdateWarFundsUI;
            GameManger.onNewWaveStarted += UpdateWaveCounterUI;
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
            if (onArmorySelect != null)
                onArmorySelect(armorSelected);
        }

        private void OnDisable()
        {
            GameManger.onWarFundsChanged -= UpdateWarFundsUI;
            GameManger.onNewWaveStarted -= UpdateWaveCounterUI;
        }
    }
}
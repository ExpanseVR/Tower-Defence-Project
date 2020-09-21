using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts.Utility;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class GameManger : MonoSingleton<GameManger>
    {
        //public static event Action onNewWaveStarted;

        [SerializeField]
        int _warFunds;

        [SerializeField]
        Tower[] _towersInArmoury = new Tower[2];

        [SerializeField]
        float _delayBetweenWaves = 5f;

        [SerializeField]
        private Transform _targetDestination;

        [SerializeField]
        private List<Wave> _waves = new List<Wave>();

        private int _currentWave = 1;

        private void Start()
        {
            StartCoroutine(StartWaves());
        }

        IEnumerator StartWaves()
        {
            //read from the current wave data
            foreach (var wave in _waves)
            {
                //onNewWaveStarted();
                EventManager.Fire("NewWaveStarted");
                //instantiate the wave
                foreach (var obj in wave.sequenceOfEnemies)
                    {
                        //instantiate them
                        SpawnManager.Instance.SpawnEnemy(obj);
                        //wait a set amount of time between spawns
                        yield return new WaitForSeconds(_waves[_currentWave].timeBetweenSpawns);
                    }
                //wait between waves
                yield return new WaitForSeconds(_delayBetweenWaves);
                _currentWave++;
            }

        }

        public Transform RequestTarget()
        {
            return _targetDestination;
        }

        public int GetWarfunds()
        {
            return _warFunds;
        }

        public void SetWarFunds (int adjustment)
        {
            _warFunds += adjustment;
            EventManager.Fire("WarFundsChanged");
        }

        public int GetWaveCount()
        {
            return _waves.Count;
        }

        public int GetCurrentWave()
        {
            return _currentWave;
        }

        public Tower GetTowerType (int buttonID)
        {
            return _towersInArmoury[buttonID];
        }
    }
}
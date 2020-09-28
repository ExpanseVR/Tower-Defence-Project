using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts.Utility;
using System;

namespace GameDevHQ.Scripts.Managers
{
    public class GameManger : MonoSingleton<GameManger>
    {
        [SerializeField]
        int _warFunds;

        [SerializeField]
        int _playerLives;

        [SerializeField]
        Tower[] _towersInArmoury = new Tower[2];

        [SerializeField]
        float _delayBetweenWaves = 5f;

        [SerializeField]
        private Transform _targetDestination;

        [SerializeField]
        private List<Wave> _waves = new List<Wave>();

        private int _currentWave = 1;
        private int _currentLives;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.EnemyCleanUp.ToString(), (Action<int>)SetWarFunds);
            EventManager.Listen(EventManager.Events.EnemyGoalReached.ToString(), (Action<int>)SetLives);
        }

        private void Start()
        {
            _currentLives = _playerLives;
            EventManager.Fire(EventManager.Events.LivesChanged.ToString(), _currentLives);
            StartCoroutine(StartWaves());
        }

        IEnumerator StartWaves()
        {
            //read from the current wave data
            foreach (var wave in _waves)
            {
                EventManager.Fire(EventManager.Events.NewWaveStarted.ToString());
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
            EventManager.Fire(EventManager.Events.WarFundsChanged.ToString());
        }

        public void SetLives (int adjustment)
        {
            _currentLives += adjustment;
            EventManager.Fire(EventManager.Events.LivesChanged.ToString(), _currentLives);
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

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.EnemyCleanUp.ToString(), (Action<int>)SetWarFunds);
            EventManager.StopListening(EventManager.Events.EnemyGoalReached.ToString(), (Action<int>)SetLives);
        }
    }
}
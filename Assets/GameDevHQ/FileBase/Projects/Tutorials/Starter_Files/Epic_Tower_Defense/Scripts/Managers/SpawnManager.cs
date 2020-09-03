using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


namespace GameDevHQ.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager _instance;
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("The GameManager is NULL.");

                return _instance;
            }
        }

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        EnemyManager _enemies;

        [SerializeField]
        int _enemiesToPool;

        int _poolReference;
        PoolManager _poolManager;//TO REMOVE AFTER CHECKING WITH JON

        private void Awake()
        {
            _instance = this;

            //enemyPool = PoolManager.Instance.CreateNewList(); CHECK WITH JON < ---WHY IS THIS NOT WORKING?
            _poolManager = FindObjectOfType<PoolManager>();
            _poolReference = _poolManager.CreateNewList(_enemies.EnemyTypes(), _enemiesToPool);
        }

        public void SpawnEnemy()
        {
            //check if their is a disable enemy in enemy pool
            //if their is retrieve enemy from the pool and set to the start
            GameObject newEnemy = _poolManager.ReturnPool(_poolReference).CheckForDisabledGameObject();

            if (newEnemy == null)
            { 
                //if not instantiate enemy
                newEnemy = _poolManager.AddToExistingList(_poolReference, _enemies.EnemyTypes());
            }

            newEnemy.transform.position = _spawnPoint.position;
            newEnemy.SetActive(true);
        }
    }
}
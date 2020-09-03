using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


namespace GameDevHQ.Scripts.Managers
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        EnemyManager _enemies;

        [SerializeField]
        int _enemiesToPool;

        int _poolReference;

        private void Start()
        {
            _poolReference = PoolManager.Instance.CreateNewList(_enemies.EnemyTypes(), _enemiesToPool);
        }

        public void SpawnEnemy()
        {
            //check if there is a disable enemy in enemy pool
            //if their is retrieve enemy from the pool and set to the start
            GameObject newEnemy = PoolManager.Instance.ReturnPool(_poolReference).CheckForDisabledGameObject();

            if (newEnemy == null)
            { 
                //if not instantiate enemy
                newEnemy = PoolManager.Instance.AddToExistingList(_poolReference, _enemies.EnemyTypes());
            }

            newEnemy.transform.position = _spawnPoint.position;
            newEnemy.SetActive(true);
        }
    }
}
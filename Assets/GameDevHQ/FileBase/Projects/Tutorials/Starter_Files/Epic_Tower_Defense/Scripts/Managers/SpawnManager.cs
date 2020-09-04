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

        int _poolReference;

        private void OnEnable()
        {
            _poolReference = PoolManager.Instance.CreateNewList();
        }

        public void SpawnEnemy(GameObject enemyToSpawn)
        {
            //check if there is a disable enemy in enemy pool
            //if their is retrieve enemy from the pool and set to the start
            GameObject newEnemy = PoolManager.Instance.ReturnPool(_poolReference).CheckForDisabledGameObject(enemyToSpawn);

            if (newEnemy == null)
            { 
                //if not instantiate enemy
                newEnemy = PoolManager.Instance.AddToExistingList(_poolReference, enemyToSpawn);
            }

            newEnemy.transform.position = _spawnPoint.position;
            newEnemy.SetActive(true);
        }
    }
}
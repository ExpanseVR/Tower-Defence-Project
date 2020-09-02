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

        ObjectPool enemyPool;
        PoolManager poolManager;//TO REMOVE AFTER CHECKING WITH JON

        private void Awake()
        {
            _instance = this;

            //enemyPool = PoolManager.Instance.GetObjectPool("Enemies"); CHECK WITH JON < ---WHY IS THIS NOT WORKING?
            poolManager = FindObjectOfType<PoolManager>();
            enemyPool = poolManager.GetObjectPool("Enemies");
        }

        public void SpawnEnemy()
        {
            //check if their is a disable enemy in enemy pool
            //if their is retrieve enemy from the pool and set to the start
            GameObject newEnemy = enemyPool.CheckForDisabledGameObject();

            if (newEnemy != null)
            {
                newEnemy.transform.position = _spawnPoint.position;
                newEnemy.SetActive(true);
            }
            else
            {
                //if not instantiate enemy
                enemyPool.CreateNew(_spawnPoint);
            }
        }
    }
}
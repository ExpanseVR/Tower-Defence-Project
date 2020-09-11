using GameDevHQ.Scripts.Utility;
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
            //Creat a list to store the spawned mechs.
            _poolReference = PoolManager.Instance.CreateNewList();
        }

        public void SpawnEnemy(GameObject enemyToSpawn)
        {
            //Spawn enemy from the pool, set position to the spawn point and make sure it is active after coming out of the pool.
            GameObject newEnemy = PoolManager.Instance.ReturnPool(_poolReference).CheckForDisabledGameObject(enemyToSpawn);

            newEnemy.transform.position = _spawnPoint.position;
            newEnemy.SetActive(true);
        }
    }
}
using GameDevHQ.Scripts.Utility;
using GameDevHQ.UI;
using UnityEngine;


namespace GameDevHQ.Scripts.Managers
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private HealthBarUI _healthBar;

        [SerializeField]
        private RectTransform _healthBarsCanvasRectT;

        [SerializeField]
        private GameObject _healthBarsParent;


        int _poolReference;
        int _spawncount = 0;

        private void OnEnable()
        {
            //Creat a list to store the spawned mechs.
            _poolReference = PoolManager.Instance.CreateNewList();
        }

        public void SpawnEnemy(GameObject enemyToSpawn)
        {
            //Spawn enemy from the pool, set position to the spawn point and make sure it is active after coming out of the pool.
            GameObject newEnemy = PoolManager.Instance.ReturnPool(_poolReference).GetGameObjectFromPool(enemyToSpawn);

            newEnemy.transform.position = _spawnPoint.position;
            newEnemy.transform.parent = this.transform;
            newEnemy.SetActive(true);

            /*GameObject newEnemyHealthBar = Instantiate(_healthBar.gameObject);
            newEnemyHealthBar.transform.SetParent(_healthBarsParent.transform, false);
            newEnemyHealthBar.GetComponent<HealthBarUI>().SetEnemyTransform(newEnemy.GetComponent<Enemy>(), _healthBarsCanvasRectT);*/
        }

        public int GetNextID()
        {
            return _spawncount++;
        }
    }
}